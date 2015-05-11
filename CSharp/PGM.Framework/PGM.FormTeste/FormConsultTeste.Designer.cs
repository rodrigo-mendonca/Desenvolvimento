namespace PGM.FormTeste
{
    partial class FormConsultTeste
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
            this.pgmButton1 = new PGM.Controls.PgmControl.PgmButton();
            this.pgmButton2 = new PGM.Controls.PgmControl.PgmButton();
            this.pgmButton3 = new PGM.Controls.PgmControl.PgmButton();
            this.SuspendLayout();
            // 
            // pgmButton1
            // 
            this.pgmButton1.Location = new System.Drawing.Point(286, 44);
            this.pgmButton1.Name = "pgmButton1";
            this.pgmButton1.Size = new System.Drawing.Size(75, 23);
            this.pgmButton1.TabIndex = 3;
            this.pgmButton1.Text = "Serializar";
            this.pgmButton1.UseVisualStyleBackColor = true;
            this.pgmButton1.Click += new System.EventHandler(this.pgmButton1_Click);
            // 
            // pgmButton2
            // 
            this.pgmButton2.Location = new System.Drawing.Point(367, 44);
            this.pgmButton2.Name = "pgmButton2";
            this.pgmButton2.Size = new System.Drawing.Size(75, 23);
            this.pgmButton2.TabIndex = 4;
            this.pgmButton2.Text = "DesSerializar";
            this.pgmButton2.UseVisualStyleBackColor = true;
            this.pgmButton2.Click += new System.EventHandler(this.pgmButton2_Click);
            // 
            // pgmButton3
            // 
            this.pgmButton3.Location = new System.Drawing.Point(13, 44);
            this.pgmButton3.Name = "pgmButton3";
            this.pgmButton3.Size = new System.Drawing.Size(75, 23);
            this.pgmButton3.TabIndex = 5;
            this.pgmButton3.Text = "pgmButton3";
            this.pgmButton3.UseVisualStyleBackColor = true;
            this.pgmButton3.Click += new System.EventHandler(this.pgmButton3_Click);
            // 
            // FormConsultTeste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 376);
            this.Controls.Add(this.pgmButton3);
            this.Controls.Add(this.pgmButton2);
            this.Controls.Add(this.pgmButton1);
            this.Name = "FormConsultTeste";
            this.Text = "Form de Teste do c#";
            this.PgmAmbient += new System.EventHandler(this.FormConsultTeste_PgmAmbient);
            this.Load += new System.EventHandler(this.FormConsultTeste_Load);
            this.Controls.SetChildIndex(this.pgmButton1, 0);
            this.Controls.SetChildIndex(this.pgmButton2, 0);
            this.Controls.SetChildIndex(this.pgmButton3, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.PgmControl.PgmButton pgmButton1;
        private Controls.PgmControl.PgmButton pgmButton2;
        private Controls.PgmControl.PgmButton pgmButton3;
    }
}