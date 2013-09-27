using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using GZM.COMAdmin.Entities;

namespace GZM.COMAdmin.WinApp
{

    public partial class frmDashBoard : Form
    {
        private bool _selectAll = false;

        #region Event
        
        /// <summary>
        /// constructeur
        /// </summary>
        public frmDashBoard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDashBoard_Load(object sender, EventArgs e)
        {
            //=== Définit les headers
            lstComApp.Columns.Add("Nom Application", 150);
            lstComApp.Columns.Add("Classes par App", 150, HorizontalAlignment.Right);
            lstComApp.Columns.Add("Total Temps reponse (ms)", 150, HorizontalAlignment.Right);
            lstComApp.Columns.Add("En Utilisation (nb appel)", 150, HorizontalAlignment.Right);

            txtMinHangTime.Text = Properties.Settings.Default.MIN_HANG_TIME.ToString();

            RefreshLstComApp();
        }
        /// <summary>
        /// Bouton Arreter les applications
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResetSelected_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirmer le SHUTDOWN sur ces applications?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                List<string> applicationsName = new List<string>();

                foreach (ListViewItem lvi in lstComApp.SelectedItems)
                {
                    applicationsName.Add(lvi.SubItems[0].Text);
                }

                bool shutdowmCompleted = ShutDownCOMApplication(applicationsName);

                //=== Si réussi on affiche un msg d'erreur
                if (shutdowmCompleted)
                {
                    MessageBox.Show("Les applications ont été arrêté.");
                }

                RefreshLstComApp();
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMinHangTime.Text))
            {
                MessageBox.Show("Le temps en (ms) est obligatoire.");
            }
            else
            {            
                RefreshLstComApp();
            }
        }

        /// <summary>
        /// Selection tous les items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            lstComApp.Focus();
            // Si on a déja tous sélection on déselection tous.
            if (_selectAll)
            {
                lstComApp.SelectedItems.Clear();
                _selectAll = false;

                btnSelectAll.Text = "Select All";
            }
            else
            {
                foreach (ListViewItem item in lstComApp.Items)
                {
                    item.Selected = true;
                }
                _selectAll = true;
                btnSelectAll.Text = "Select None";
            }

        }

        #endregion

        #region Methods

        /// <summary>
        /// Rafraichissement de la liste
        /// </summary>
        private void RefreshLstComApp()
        {
            //Si la valeur du refresh n'est pas null, pour blinder
            if (!String.IsNullOrEmpty(txtMinHangTime.Text))
            {

                //lstComApp.Clear();
                lstComApp.Items.Clear();

                EntityFactory comH = new EntityFactory();

                //=== Peuple la liste
                int i = 0;
                foreach (ComApp com in comH.GetCOMs(long.Parse(txtMinHangTime.Text)))
                {
                    ListViewItem itm = new ListViewItem(new string[] { com.ApplicationName, com.NbClass.ToString(), com.TotalResponseTime.ToString(), com.TotalInCall.ToString() });
                    lstComApp.Items.Add(itm);

                    //com.MinHangTime = long.Parse(txtMinHangTime.Text);

                    //=== Si l'application est considérer gelé on affiche la ligne en rouge
                    if (com.isHang) lstComApp.Items[i].BackColor = Color.Red;
                    i++;
                }
            }
        }

        /// <summary>
        /// ShutDown des Com
        /// </summary>
        /// <param name="applicationsName">Si Null on Kill tous les COM</param>
        private bool ShutDownCOMApplication(IList applicationsName)
        {
            bool shutdowmCompleted = false;
            
            //TODO === Affiche un msg durant l'éxecution

            EntityFactory ef = new EntityFactory();
            try
            {
                //=== Shutdown de tous
                if (applicationsName == null)
                {
                    shutdowmCompleted = ef.ShutDownAllCOMApplication();
                }
                else
                {
                    shutdowmCompleted = ef.ShutDownCOMApplication(applicationsName);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return shutdowmCompleted;
        }
        #endregion

    }
}
