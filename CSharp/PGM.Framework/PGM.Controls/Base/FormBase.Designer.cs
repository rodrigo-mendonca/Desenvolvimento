namespace PGM.Controls
{
    partial class FormBase
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
            this.components = new System.ComponentModel.Container();
            this.lblinkAjuda = new System.Windows.Forms.LinkLabel();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.pctLoading = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pctLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // lblinkAjuda
            // 
            this.lblinkAjuda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblinkAjuda.AutoSize = true;
            this.lblinkAjuda.Location = new System.Drawing.Point(235, 2);
            this.lblinkAjuda.Name = "lblinkAjuda";
            this.lblinkAjuda.Size = new System.Drawing.Size(34, 13);
            this.lblinkAjuda.TabIndex = 50;
            this.lblinkAjuda.TabStop = true;
            this.lblinkAjuda.Text = "Ajuda";
            this.lblinkAjuda.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblinkAjuda_LinkClicked);
            // 
            // TopPanel
            // 
            this.TopPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TopPanel.Location = new System.Drawing.Point(-2, 0);
            this.TopPanel.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(274, 20);
            this.TopPanel.TabIndex = 1;
            // 
            // Timer
            // 
            this.Timer.Interval = 1000;
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // pctLoading
            // 
            this.pctLoading.Image = global::PGM.Controls.Properties.Resources.Loading;
            this.pctLoading.Location = new System.Drawing.Point(0, 2);
            this.pctLoading.Name = "pctLoading";
            this.pctLoading.Size = new System.Drawing.Size(18, 17);
            this.pctLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctLoading.TabIndex = 51;
            this.pctLoading.TabStop = false;
            this.pctLoading.Visible = false;
            // 
            // FormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 262);
            this.Controls.Add(this.pctLoading);
            this.Controls.Add(this.lblinkAjuda);
            this.Controls.Add(this.TopPanel);
            this.KeyPreview = true;
            this.Name = "FormBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormBase";
            this.Load += new System.EventHandler(this.FormBase_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormBase_KeyPress);
            this.Resize += new System.EventHandler(this.FormBase_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pctLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
            this.CloseEsc = true;

        }

        #endregion

        private System.Windows.Forms.LinkLabel lblinkAjuda;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.PictureBox pctLoading;
        private System.Windows.Forms.Timer Timer;
    }
}