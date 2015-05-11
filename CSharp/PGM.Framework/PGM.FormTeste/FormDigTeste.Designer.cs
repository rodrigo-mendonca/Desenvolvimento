namespace PGM.FormTeste
{
    partial class FormDigTeste
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
            this.txtID = new PGM.Controls.PgmControl.PgmTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtValor = new PGM.Controls.PgmControl.PgmTextBox();
            this.txtDesc = new PGM.Controls.PgmControl.PgmTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pgmButton1 = new PGM.Controls.PgmControl.PgmButton();
            this.txtData = new PGM.Controls.PgmControl.PgmTextBox();
            this.SuspendLayout();
            // 
            // txtID
            // 
            this.txtID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(225)))));
            this.txtID.BlankIfZero = true;
            this.txtID.DataType = PGM.Controls.PgmControl.PgmTextBox.Type.Integer;
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(73, 47);
            this.txtID.Name = "txtID";
            this.txtID.Obrigatory = true;
            this.txtID.Size = new System.Drawing.Size(98, 20);
            this.txtID.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Id.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Data";
            // 
            // txtValor
            // 
            this.txtValor.BackColor = System.Drawing.Color.White;
            this.txtValor.BlankIfZero = true;
            this.txtValor.DataType = PGM.Controls.PgmControl.PgmTextBox.Type.Decimal;
            this.txtValor.Location = new System.Drawing.Point(73, 99);
            this.txtValor.Name = "txtValor";
            this.txtValor.Obrigatory = false;
            this.txtValor.Size = new System.Drawing.Size(98, 20);
            this.txtValor.TabIndex = 27;
            // 
            // txtDesc
            // 
            this.txtDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(225)))));
            this.txtDesc.BlankIfZero = false;
            this.txtDesc.DataType = PGM.Controls.PgmControl.PgmTextBox.Type.String;
            this.txtDesc.Location = new System.Drawing.Point(73, 73);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Obrigatory = true;
            this.txtDesc.Size = new System.Drawing.Size(98, 20);
            this.txtDesc.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Valor";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Desc.";
            // 
            // pgmButton1
            // 
            this.pgmButton1.Location = new System.Drawing.Point(293, 62);
            this.pgmButton1.Name = "pgmButton1";
            this.pgmButton1.Size = new System.Drawing.Size(75, 23);
            this.pgmButton1.TabIndex = 32;
            this.pgmButton1.Text = "pgmButton1";
            this.pgmButton1.UseVisualStyleBackColor = true;
            // 
            // txtData
            // 
            this.txtData.BackColor = System.Drawing.Color.White;
            this.txtData.BlankIfZero = true;
            this.txtData.DataType = PGM.Controls.PgmControl.PgmTextBox.Type.Date;
            this.txtData.Location = new System.Drawing.Point(73, 125);
            this.txtData.Name = "txtData";
            this.txtData.Obrigatory = false;
            this.txtData.Size = new System.Drawing.Size(98, 20);
            this.txtData.TabIndex = 52;
            // 
            // FormDigTeste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 340);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.pgmButton1);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormDigTeste";
            this.Text = "FormDigTeste";
            this.PgmRegister += new System.EventHandler(this.FormDigTeste_PgmRegister);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtDesc, 0);
            this.Controls.SetChildIndex(this.txtValor, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtID, 0);
            this.Controls.SetChildIndex(this.pgmButton1, 0);
            this.Controls.SetChildIndex(this.txtData, 0);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Controls.PgmControl.PgmTextBox txtID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private Controls.PgmControl.PgmTextBox txtValor;
        private Controls.PgmControl.PgmTextBox txtDesc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Controls.PgmControl.PgmButton pgmButton1;
        private Controls.PgmControl.PgmTextBox txtData;
    }
}