namespace USBBackupWindows
{
    partial class USBBackupWindows
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
            this.txtOrigen = new System.Windows.Forms.TextBox();
            this.cmdOri = new System.Windows.Forms.Button();
            this.cmdDes = new System.Windows.Forms.Button();
            this.txtDestino = new System.Windows.Forms.TextBox();
            this.cmdBackup = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // txtOrigen
            // 
            this.txtOrigen.Location = new System.Drawing.Point(12, 12);
            this.txtOrigen.Name = "txtOrigen";
            this.txtOrigen.ReadOnly = true;
            this.txtOrigen.Size = new System.Drawing.Size(456, 20);
            this.txtOrigen.TabIndex = 0;
            // 
            // cmdOri
            // 
            this.cmdOri.Location = new System.Drawing.Point(468, 12);
            this.cmdOri.Name = "cmdOri";
            this.cmdOri.Size = new System.Drawing.Size(21, 19);
            this.cmdOri.TabIndex = 1;
            this.cmdOri.Text = "...";
            this.cmdOri.UseVisualStyleBackColor = true;
            this.cmdOri.Click += new System.EventHandler(this.cmdOri_Click);
            // 
            // cmdDes
            // 
            this.cmdDes.Location = new System.Drawing.Point(468, 40);
            this.cmdDes.Name = "cmdDes";
            this.cmdDes.Size = new System.Drawing.Size(21, 20);
            this.cmdDes.TabIndex = 3;
            this.cmdDes.Text = "...";
            this.cmdDes.UseVisualStyleBackColor = true;
            this.cmdDes.Click += new System.EventHandler(this.cmdDes_Click);
            // 
            // txtDestino
            // 
            this.txtDestino.Location = new System.Drawing.Point(12, 41);
            this.txtDestino.Name = "txtDestino";
            this.txtDestino.ReadOnly = true;
            this.txtDestino.Size = new System.Drawing.Size(456, 20);
            this.txtDestino.TabIndex = 2;
            // 
            // cmdBackup
            // 
            this.cmdBackup.Location = new System.Drawing.Point(414, 67);
            this.cmdBackup.Name = "cmdBackup";
            this.cmdBackup.Size = new System.Drawing.Size(75, 23);
            this.cmdBackup.TabIndex = 4;
            this.cmdBackup.Text = "Backup";
            this.cmdBackup.UseVisualStyleBackColor = true;
            this.cmdBackup.Click += new System.EventHandler(this.cmdBackup_Click);
            // 
            // USBBackupWindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 100);
            this.Controls.Add(this.cmdBackup);
            this.Controls.Add(this.cmdDes);
            this.Controls.Add(this.txtDestino);
            this.Controls.Add(this.cmdOri);
            this.Controls.Add(this.txtOrigen);
            this.Name = "USBBackupWindows";
            this.Text = "USBBackup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOrigen;
        private System.Windows.Forms.Button cmdOri;
        private System.Windows.Forms.Button cmdDes;
        private System.Windows.Forms.TextBox txtDestino;
        private System.Windows.Forms.Button cmdBackup;
        private System.Windows.Forms.Timer timer1;

    }
}

