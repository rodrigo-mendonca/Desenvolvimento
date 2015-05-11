namespace PGM.Controls.PgmControl
{
    partial class PgmF4Textbox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pgmTextBox1 = new PGM.Controls.PgmControl.PgmTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pgmTextBox1
            // 
            this.pgmTextBox1.BackColor = System.Drawing.Color.White;
            this.pgmTextBox1.BlankIfZero = false;
            this.pgmTextBox1.DataType = PGM.Controls.PgmControl.PgmTextBox.Type.String;
            this.pgmTextBox1.Location = new System.Drawing.Point(0, 0);
            this.pgmTextBox1.Name = "pgmTextBox1";
            this.pgmTextBox1.Obrigatory = false;
            this.pgmTextBox1.Size = new System.Drawing.Size(98, 20);
            this.pgmTextBox1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(98, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(19, 19);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // PgmF4Textbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pgmTextBox1);
            this.Name = "PgmF4Textbox";
            this.Size = new System.Drawing.Size(118, 19);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PgmControl.PgmTextBox pgmTextBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
