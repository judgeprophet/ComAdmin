namespace GZM.COMAdmin.WinApp
{
    /// <summary>
    /// 
    /// </summary>
    partial class frmDashBoard
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnResetSelected = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lstComApp = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMinHangTime = new System.Windows.Forms.Label();
            this.txtMinHangTime = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblResetNote = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(15, 481);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(87, 23);
            this.btnSelectAll.TabIndex = 1;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnResetSelected
            // 
            this.btnResetSelected.Location = new System.Drawing.Point(369, 481);
            this.btnResetSelected.Name = "btnResetSelected";
            this.btnResetSelected.Size = new System.Drawing.Size(88, 23);
            this.btnResetSelected.TabIndex = 2;
            this.btnResetSelected.Text = "Arrêter";
            this.btnResetSelected.UseVisualStyleBackColor = true;
            this.btnResetSelected.Click += new System.EventHandler(this.btnResetSelected_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(731, 507);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(75, 23);
            this.btnQuit.TabIndex = 3;
            this.btnQuit.Text = "Quitter";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(699, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lstComApp
            // 
            this.lstComApp.CausesValidation = false;
            this.lstComApp.FullRowSelect = true;
            this.lstComApp.Location = new System.Drawing.Point(15, 36);
            this.lstComApp.Name = "lstComApp";
            this.lstComApp.Size = new System.Drawing.Size(791, 439);
            this.lstComApp.TabIndex = 5;
            this.lstComApp.UseCompatibleStateImageBehavior = false;
            this.lstComApp.View = System.Windows.Forms.View.Details;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Application(s) chargée(s) :";
            // 
            // lblMinHangTime
            // 
            this.lblMinHangTime.AutoSize = true;
            this.lblMinHangTime.Location = new System.Drawing.Point(366, 13);
            this.lblMinHangTime.Name = "lblMinHangTime";
            this.lblMinHangTime.Size = new System.Drawing.Size(221, 13);
            this.lblMinHangTime.TabIndex = 7;
            this.lblMinHangTime.Text = "Temps (ms) min. pour App Inactive (en rouge)";
            // 
            // txtMinHangTime
            // 
            this.txtMinHangTime.Location = new System.Drawing.Point(593, 10);
            this.txtMinHangTime.Name = "txtMinHangTime";
            this.txtMinHangTime.Size = new System.Drawing.Size(100, 20);
            this.txtMinHangTime.TabIndex = 8;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(487, 481);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 9;
            this.progressBar1.Visible = false;
            // 
            // lblResetNote
            // 
            this.lblResetNote.AutoSize = true;
            this.lblResetNote.Location = new System.Drawing.Point(15, 512);
            this.lblResetNote.Name = "lblResetNote";
            this.lblResetNote.Size = new System.Drawing.Size(520, 13);
            this.lblResetNote.TabIndex = 10;
            this.lblResetNote.Text = "NOTE: L\'arrêt de composantes peut prendre plusieurs minutes.  Veuillez attendre l" +
                "e message de confirmation.";
            // 
            // frmDashBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 535);
            this.Controls.Add(this.lblResetNote);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.txtMinHangTime);
            this.Controls.Add(this.lblMinHangTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstComApp);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnResetSelected);
            this.Controls.Add(this.btnSelectAll);
            this.Name = "frmDashBoard";
            this.Text = "COMAdmin - DashBoard";
            this.Load += new System.EventHandler(this.frmDashBoard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnResetSelected;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ListView lstComApp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMinHangTime;
        private System.Windows.Forms.TextBox txtMinHangTime;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblResetNote;

    }
}

