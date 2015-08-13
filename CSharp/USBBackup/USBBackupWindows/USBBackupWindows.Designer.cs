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
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.lblMsg = new System.Windows.Forms.Label();
            this.sprMinutos = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.sprMinutos)).BeginInit();
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
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(12, 78);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 13);
            this.lblMsg.TabIndex = 5;
            // 
            // sprMinutos
            // 
            this.sprMinutos.Location = new System.Drawing.Point(302, 70);
            this.sprMinutos.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.sprMinutos.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.sprMinutos.Name = "sprMinutos";
            this.sprMinutos.Size = new System.Drawing.Size(50, 20);
            this.sprMinutos.TabIndex = 6;
            this.sprMinutos.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(254, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Periodo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(358, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Minuto(s)";
            // 
            // USBBackupWindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 100);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sprMinutos);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.cmdBackup);
            this.Controls.Add(this.cmdDes);
            this.Controls.Add(this.txtDestino);
            this.Controls.Add(this.cmdOri);
            this.Controls.Add(this.txtOrigen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "USBBackupWindows";
            this.Text = "USBBackup";
            ((System.ComponentModel.ISupportInitialize)(this.sprMinutos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOrigen;
        private System.Windows.Forms.Button cmdOri;
        private System.Windows.Forms.Button cmdDes;
        private System.Windows.Forms.TextBox txtDestino;
        private System.Windows.Forms.Button cmdBackup;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.NumericUpDown sprMinutos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

    }
}

