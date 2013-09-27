using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;

using GZM.COMAdmin.Entities;

namespace GZM.COMAdmin.WebApp
{
    public partial class _Default : System.Web.UI.Page
    {
        #region Event
        /// <summary>
        /// Page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                txtMinHangTime.Text = Properties.Settings.Default.MIN_HANG_TIME.ToString();
                RefreshLstComApp();                
            }
        }

        /// <summary>
        /// Bouton Refresh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            //Si la valeur du refresh est null on affiche un msg
            if (String.IsNullOrEmpty(txtMinHangTime.Text))
            {
                StringBuilder cs = new StringBuilder();
                cs.Append("alert('Le temps en (ms) est obligatoire.');");
                ClientScript.RegisterStartupScript(GetType(), "msgErreur", cs.ToString(), true);
                
            }
            else
            {
                RefreshLstComApp();
            }
        }

        /// <summary>
        /// Bouton Shutdown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnResetSelected_Click(object sender, EventArgs e)
        {
            EntityFactory ef = new EntityFactory();

            //TODO Mettre une confirmation JS
            //if (MessageBox.Show("Confirmer le SHUTDOWN sur ces applications?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
                List<string> applicationsName = new List<string>();


                //== Crée la liste des applications à arrêter
                for (int i = 0; i < gvComApp.Rows.Count; i++)
                {
                    CheckBox chbTemp = (CheckBox)gvComApp.Rows[i].FindControl("chkSelect");

                    if (chbTemp.Checked)
                    {
                        applicationsName.Add(gvComApp.Rows[i].Cells[1].Text);
                    }

                }
                
                //=== Arret des applications sélectionnées
                bool shutdowmCompleted = ef.ShutDownCOMApplication(applicationsName);

                //=== Si réussi on affiche un msg d'erreur
                if (shutdowmCompleted)
                {
                    StringBuilder cs = new StringBuilder();
                    cs.Append("alert('Les applications ont été arrêté.');");
                    ClientScript.RegisterStartupScript(GetType(),"msgConfirmation", cs.ToString(), true);
                }
            
            RefreshLstComApp();
        }

        /// <summary>
        /// Pour chacune des lignes créés on execute
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvComApp_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //Il s'agit d'un ligne (pas d'un header)
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // determine si l'application est gelé
                bool isHang = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "isHang"));

                //Si l'application est gele on affiche la ligne en rouge
                if (isHang)
                {
                    e.Row.BackColor = System.Drawing.Color.Red;
                }
            }

        }

        #endregion

        #region Method

        /// <summary>
        /// Rafraichissement de la liste
        /// </summary>
        private void RefreshLstComApp()
        {
            //Si la valeur du refresh n'est pas null, pour blinder
            if (!String.IsNullOrEmpty(txtMinHangTime.Text))
            {
                EntityFactory comH = new EntityFactory();
                gvComApp.DataSource = comH.GetCOMs(long.Parse(txtMinHangTime.Text));
                gvComApp.DataBind();
            }
        }
        
        #endregion


    }
}
