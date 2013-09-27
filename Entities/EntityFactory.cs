using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;

using COMAdmin;
//using EGILHCOMTRACKERLib;


namespace GZM.COMAdmin.Entities
{
    /// <summary>
    /// AUTEUR: SÉBASTIEN VINCENT
    /// DATE : 2009-03-18
    /// BUT : Bibliothèque de fonction de manipulation des COM+
    /// </summary>
    public class EntityFactory
    {
        #region Method
        #region Get COM+ Applications
        /// <summary>
        /// Collection des Objets COM+
        /// </summary>
        /// <returns>Collection COMAdmin object Com+</returns>
        internal COMAdminCatalogCollection GetCollection()
        {
            COMAdminCatalogCollection objCollection = null;
            try
            {
                //ICOMAdminCatalog objAdmin = new COMAdminCatalog();
                //HACK pour Windows 2000
                ICOMAdminCatalog objAdmin = (ICOMAdminCatalog)Activator.CreateInstance(Type.GetTypeFromProgID("COMAdmin.COMAdminCatalog"));
                //COMAdminCatalogCollection objRoot = (COMAdminCatalogCollection)objAdmin.Connect("");

                objCollection = (COMAdminCatalogCollection)objAdmin.GetCollection("Applications");                
                objCollection.Populate();

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return objCollection;
        }

        /// <summary>
        /// Trouve le noms des applications définit dans COM+ Services
        /// </summary>
        /// <returns></returns>
        public ComApps GetNameListCom()
        {
            ComApps coms = new ComApps();

            try
            {
                COMAdminCatalogCollection objCollection = GetCollection();

                //=== Boucle tout les objets COM+
                foreach (ICatalogObject aco in objCollection)
                {

                    ComApp com = new ComApp();

                    string comName = aco.Name.ToString();

                    //=== Si un Service est lié au com.  C'est un COM Systeme, on le prend pas
                    //=== Si sont nom est plus petit que 4 caractères c'est un custom, on le récupere
                    //=== Si un COM s'appele MS ou IIS ou COM+ on l'ignore (d'où la vérification sur le nombre de caractères, 
                    //===                              car pour faire le check sur COM+ on doit vérifier si la string a plus de 4 caractères)

                    //HACK pour Windows 2000
                    string serviceName = ""; //=== Sous Windows 2000 le service name n'est pas pris en charge
                    //string serviceName = aco.get_Value("ServiceName").ToString();

                    if (comName.Length < 4 || (comName != "System Application" && String.IsNullOrEmpty(serviceName) && comName.Substring(0, 3) != "IIS" && comName.Substring(0, 4) != "COM+" && comName.Substring(0, 3) != "MS ")) //=== Exclusion
                    {
                        com.ApplicationName = comName;
                        com.ApplicationKey = aco.Key.ToString().ToUpper();

                        coms.Add(com);
                    }
                    
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Source + ' ' + ex.Message + ' ' + ex.StackTrace);
            }

            return coms;
        }
        #endregion

        #region Get COM+ Applications / Components - Classes
        /// <summary>
        /// Extrait une collection d'objet Com trouvé sur la machine
        /// </summary>
        /// <param name="minHangTime">temps (ms) minimum pour marqué une application comme gelé</param>
        /// <returns>collections</returns>
        public ComApps GetCOMs(long minHangTime)
        {

            //Match des Clés pour trouvé le nom de l'application
            ComApps comName = new ComApps();
            comName = GetNameListCom();

            // Get the call statistics
            //HACK pour Windows 2000
            EGILHCOMTRACKERLib.IComTracker comTracker = (EGILHCOMTRACKERLib.IComTracker)Activator.CreateInstance(Type.GetTypeFromProgID("Egilh.ComTracker.1"));

            string statistics = comTracker.getStatistics();

            // Load the statistics in a DOM and loop on the call times
            XmlDocument xmlDoc = new XmlDocument();
            #region Exemple du XML retourné
            //<application>
            //<guid>{02D4B3F1-FD88-11D1-960D-00805FC79235}</guid>
            //<ID>2</ID>
            //<processID>2312</processID>
            //<statistics>
            //    <callsPerSecond>0</callsPerSecond>
            //    <totalCalls>0</totalCalls>
            //    <totalClasses>3</totalClasses>
            //    <totalInstances>2</totalInstances>
            //</statistics>
            //<classes>
            //<class>
            //    <progID>COMSVCS.TrackerServer</progID>
            //    <bound>3</bound>
            //    <inCall>0</inCall>
            //    <pooled>-1</pooled>
            //    <references>3</references>
            //    <responseTime>0</responseTime>
            //    <callsCompleted>0</callsCompleted>
            //    <callsFailed>0</callsFailed>
            //</class>
            //</classes>
            //</application>
            #endregion

            xmlDoc.LoadXml(statistics);

            ComApps appComs = new ComApps();

            //=== Pour chacune des applications trouvé dans le XML
            foreach (XmlNode appNode in xmlDoc.SelectNodes("//applications/application"))
            {
                // Get class info
                ComApp appComFound = new ComApp();
                ComApp appCom = new ComApp();

                string applicationName = "";
                string applicationKey = appNode.SelectSingleNode("guid").InnerText.ToUpper();

                //== Trouve le nom de l'application selon la clé
                appComFound = comName.Find(delegate(ComApp t) { return t.ApplicationKey.ToUpper() == applicationKey.ToUpper(); });

                //=== Si l'application est trouvé on l'ajoute à la collection
                if (appComFound != null)
                {
                    applicationName = appComFound.ApplicationName;

                    appCom.ApplicationKey = applicationKey;
                    appCom.ApplicationName = applicationName;
                    appCom.MinHangTime = minHangTime;
                    
                    //TODO === Une classe COM devrait devenir une collection qui apartient à la classe ComApp
                    //=== Cumule chaque classe par application
                    foreach (XmlNode classNode in appNode.SelectNodes(".//classes/class"))
                    {
                        appCom.NbClass++;
                        appCom.TotalInCall += System.Convert.ToInt32(classNode.SelectSingleNode("inCall").InnerText);
                        appCom.TotalResponseTime += System.Convert.ToInt64(classNode.SelectSingleNode("responseTime").InnerText);
                    }
                    appComs.Add(appCom);
                }
            }
            return appComs;
        }
    

        #endregion

        #region Shutting Down  COM+ Application

        /// <summary>
        /// ShutDown Tous les Component
        /// </summary>
        /// <returns></returns>
        public bool ShutDownAllCOMApplication()
        {
            //TODO Se basé sur le contenu de la ListeView plutot que refaire un Query 
            IList appsName = GetNameListCom();
            return ShutDownCOMApplication(appsName);
        }

        /// <summary>
        /// ShutDown des Component
        /// </summary>
        /// <param name="applicationsName"></param>
        /// <returns></returns>
        public bool ShutDownCOMApplication(IList applicationsName)
        {
            bool ShuttingDown = false;
            try
            {
                //HACK pour Windows 2000
                ICOMAdminCatalog objAdmin = (ICOMAdminCatalog)Activator.CreateInstance(Type.GetTypeFromProgID("COMAdmin.COMAdminCatalog"));

                foreach (string appName in applicationsName)
                {
                    objAdmin.ShutdownApplication(appName);
                }

                ShuttingDown = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return ShuttingDown;
        }
        #endregion
        #endregion
    }
}
