namespace BaseConvert
{
    partial class frmBaseConvert
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
            this.txtIN = new System.Windows.Forms.TextBox();
            this.txtOUT = new System.Windows.Forms.TextBox();
            this.cboLIST1 = new System.Windows.Forms.ComboBox();
            this.cboLIST2 = new System.Windows.Forms.ComboBox();
            this.lblPARA = new System.Windows.Forms.Label();
            this.cmdCONVERT = new System.Windows.Forms.Button();
            this.chkREVERT = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtIN
            // 
            this.txtIN.Location = new System.Drawing.Point(13, 13);
            this.txtIN.Multiline = true;
            this.txtIN.Name = "txtIN";
            this.txtIN.Size = new System.Drawing.Size(368, 230);
            this.txtIN.TabIndex = 0;
            // 
            // txtOUT
            // 
            this.txtOUT.BackColor = System.Drawing.Color.White;
            this.txtOUT.Location = new System.Drawing.Point(12, 303);
            this.txtOUT.Multiline = true;
            this.txtOUT.Name = "txtOUT";
            this.txtOUT.ReadOnly = true;
            this.txtOUT.Size = new System.Drawing.Size(369, 208);
            this.txtOUT.TabIndex = 1;
            // 
            // cboLIST1
            // 
            this.cboLIST1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLIST1.FormattingEnabled = true;
            this.cboLIST1.Location = new System.Drawing.Point(13, 252);
            this.cboLIST1.Name = "cboLIST1";
            this.cboLIST1.Size = new System.Drawing.Size(121, 21);
            this.cboLIST1.TabIndex = 2;
            // 
            // cboLIST2
            // 
            this.cboLIST2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLIST2.FormattingEnabled = true;
            this.cboLIST2.Location = new System.Drawing.Point(182, 252);
            this.cboLIST2.Name = "cboLIST2";
            this.cboLIST2.Size = new System.Drawing.Size(121, 21);
            this.cboLIST2.TabIndex = 3;
            // 
            // lblPARA
            // 
            this.lblPARA.AutoSize = true;
            this.lblPARA.BackColor = System.Drawing.Color.Silver;
            this.lblPARA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPARA.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblPARA.Location = new System.Drawing.Point(140, 254);
            this.lblPARA.Name = "lblPARA";
            this.lblPARA.Size = new System.Drawing.Size(36, 15);
            this.lblPARA.TabIndex = 4;
            this.lblPARA.Text = "<<->>";
            this.lblPARA.Click += new System.EventHandler(this.lblPARA_Click);
            // 
            // cmdCONVERT
            // 
            this.cmdCONVERT.Location = new System.Drawing.Point(307, 250);
            this.cmdCONVERT.Name = "cmdCONVERT";
            this.cmdCONVERT.Size = new System.Drawing.Size(74, 23);
            this.cmdCONVERT.TabIndex = 5;
            this.cmdCONVERT.Text = "Convert!";
            this.cmdCONVERT.UseVisualStyleBackColor = true;
            this.cmdCONVERT.Click += new System.EventHandler(this.cmdCONVERT_Click);
            // 
            // chkREVERT
            // 
            this.chkREVERT.AutoSize = true;
            this.chkREVERT.Location = new System.Drawing.Point(13, 280);
            this.chkREVERT.Name = "chkREVERT";
            this.chkREVERT.Size = new System.Drawing.Size(99, 17);
            this.chkREVERT.TabIndex = 6;
            this.chkREVERT.Text = "Reverse results";
            this.chkREVERT.UseVisualStyleBackColor = true;
            // 
            // frmBaseConvert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 523);
            this.Controls.Add(this.chkREVERT);
            this.Controls.Add(this.cmdCONVERT);
            this.Controls.Add(this.lblPARA);
            this.Controls.Add(this.cboLIST2);
            this.Controls.Add(this.cboLIST1);
            this.Controls.Add(this.txtOUT);
            this.Controls.Add(this.txtIN);
            this.Name = "frmBaseConvert";
            this.Text = "Convert Bases";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIN;
        private System.Windows.Forms.TextBox txtOUT;
        private System.Windows.Forms.ComboBox cboLIST1;
        private System.Windows.Forms.ComboBox cboLIST2;
        private System.Windows.Forms.Label lblPARA;
        private System.Windows.Forms.Button cmdCONVERT;
        private System.Windows.Forms.CheckBox chkREVERT;
    }
}

