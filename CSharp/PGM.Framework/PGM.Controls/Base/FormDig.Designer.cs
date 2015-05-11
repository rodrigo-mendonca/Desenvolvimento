namespace PGM.Controls
{
    partial class FormDig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdOk = new PGM.Controls.PgmControl.PgmButton();
            this.cmdCancelar = new PGM.Controls.PgmControl.PgmButton();
            this.lblLinkID = new System.Windows.Forms.LinkLabel();
            this.BottonPanel = new System.Windows.Forms.Panel();
            this.BottonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdOk
            // 
            this.cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdOk.Location = new System.Drawing.Point(96, 9);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(75, 23);
            this.cmdOk.TabIndex = 2;
            this.cmdOk.Text = "Ok";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdCancelar
            // 
            this.cmdCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancelar.Location = new System.Drawing.Point(245, 9);
            this.cmdCancelar.Name = "cmdCancelar";
            this.cmdCancelar.Size = new System.Drawing.Size(75, 23);
            this.cmdCancelar.TabIndex = 3;
            this.cmdCancelar.Text = "Cancelar";
            this.cmdCancelar.UseVisualStyleBackColor = true;
            this.cmdCancelar.Click += new System.EventHandler(this.cmdCancelar_Click);
            // 
            // lblLinkID
            // 
            this.lblLinkID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLinkID.AutoSize = true;
            this.lblLinkID.Location = new System.Drawing.Point(10, 14);
            this.lblLinkID.Name = "lblLinkID";
            this.lblLinkID.Size = new System.Drawing.Size(56, 13);
            this.lblLinkID.TabIndex = 4;
            this.lblLinkID.TabStop = true;
            this.lblLinkID.Text = "ID: (Novo)";
            // 
            // BottonPanel
            // 
            this.BottonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BottonPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.BottonPanel.Controls.Add(this.cmdCancelar);
            this.BottonPanel.Controls.Add(this.lblLinkID);
            this.BottonPanel.Controls.Add(this.cmdOk);
            this.BottonPanel.Location = new System.Drawing.Point(-2, 293);
            this.BottonPanel.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.BottonPanel.Name = "BottonPanel";
            this.BottonPanel.Size = new System.Drawing.Size(421, 42);
            this.BottonPanel.TabIndex = 5;
            // 
            // FormDig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 336);
            this.Controls.Add(this.BottonPanel);
            this.Name = "FormDig";
            this.Text = "FormDig";
            this.PgmLoadingTimer += new System.EventHandler(this.FormDig_PgmLoadingTimer);
            this.Load += new System.EventHandler(this.FormDig_Load);
            this.Resize += new System.EventHandler(this.FormDig_Resize);
            this.Controls.SetChildIndex(this.BottonPanel, 0);
            this.BottonPanel.ResumeLayout(false);
            this.BottonPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PgmControl.PgmButton cmdOk;
        private PgmControl.PgmButton cmdCancelar;
        private System.Windows.Forms.LinkLabel lblLinkID;
        private System.Windows.Forms.Panel BottonPanel;

    }
}