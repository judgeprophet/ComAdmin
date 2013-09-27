using System;
using System.Collections.Generic;
using System.Text;

using System.Configuration;

namespace GZM.COMAdmin.Entities
{
    /// <summary>
    /// Classe des Components Applications
    /// </summary>
    public class ComApp : Entity
    {

        private long _minHangTime;

        /// <summary>
        /// Nom de l'application
        /// </summary>
        public string ApplicationName { get; set; }
        
        /// <summary>
        /// GUID de l'application
        /// </summary>
        public string ApplicationKey { get; set; }
        
        /// <summary>
        /// Nombre de classe sous l'application
        /// </summary>
        public int NbClass { get; set; }
        
        /// <summary>
        /// temps total en (MS) de l'activité des classes
        /// </summary>
        public long TotalResponseTime { get; set; }
        
        /// <summary>
        /// Nombre total des appels en cours aux classes
        /// </summary>
        public int TotalInCall { get; set; }

        /// <summary>
        /// Temps requis pour considérer le com comme "gelé"
        /// </summary>
        public long MinHangTime 
        { 
            get
            {
                return _minHangTime;
            }
            set
            {
                _minHangTime = value;
            }
        }

        /// <summary>
        /// L'application est-elle considéré gelé?
        /// </summary>
        public bool isHang
        {
            get
            {
                if (TotalResponseTime >= _minHangTime)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
