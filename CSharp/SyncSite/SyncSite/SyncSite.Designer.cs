namespace SyncSite
{
    partial class SyncSite
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
            this.Time = new System.Windows.Forms.Timer(this.components);
            this.cmdIniciar = new System.Windows.Forms.Button();
            this.txtHora = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // Time
            // 
            this.Time.Interval = 60000;
            this.Time.Tick += new System.EventHandler(this.Time_Tick);
            // 
            // cmdIniciar
            // 
            this.cmdIniciar.Location = new System.Drawing.Point(12, 38);
            this.cmdIniciar.Name = "cmdIniciar";
            this.cmdIniciar.Size = new System.Drawing.Size(155, 23);
            this.cmdIniciar.TabIndex = 0;
            this.cmdIniciar.Text = "Iniciar";
            this.cmdIniciar.UseVisualStyleBackColor = true;
            this.cmdIniciar.Click += new System.EventHandler(this.cmdIniciar_Click);
            // 
            // txtHora
            // 
            this.txtHora.Location = new System.Drawing.Point(12, 12);
            this.txtHora.Mask = "00:00";
            this.txtHora.Name = "txtHora";
            this.txtHora.Size = new System.Drawing.Size(155, 20);
            this.txtHora.TabIndex = 1;
            this.txtHora.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtHora.ValidatingType = typeof(System.DateTime);
            // 
            // SyncSite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(179, 73);
            this.Controls.Add(this.txtHora);
            this.Controls.Add(this.cmdIniciar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "SyncSite";
            this.Text = "SyncSite";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer Time;
        private System.Windows.Forms.Button cmdIniciar;
        private System.Windows.Forms.MaskedTextBox txtHora;
    }
}