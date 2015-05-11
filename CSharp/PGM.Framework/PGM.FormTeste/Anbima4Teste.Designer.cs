namespace PGM.FormTeste
{
    partial class Anbima4Teste
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
            this.Grid = new PGM.Controls.PgmControl.PgmDataGrid();
            this.pgmButton1 = new PGM.Controls.PgmControl.PgmButton();
            this.ListTopicos = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // Grid
            // 
            this.Grid.IsSortable = true;
            this.Grid.Location = new System.Drawing.Point(219, 85);
            this.Grid.Name = "Grid";
            this.Grid.Size = new System.Drawing.Size(578, 290);
            this.Grid.TabIndex = 2;
            // 
            // pgmButton1
            // 
            this.pgmButton1.Location = new System.Drawing.Point(12, 54);
            this.pgmButton1.Name = "pgmButton1";
            this.pgmButton1.Size = new System.Drawing.Size(75, 23);
            this.pgmButton1.TabIndex = 3;
            this.pgmButton1.Text = "pgmButton1";
            this.pgmButton1.UseVisualStyleBackColor = true;
            this.pgmButton1.Click += new System.EventHandler(this.pgmButton1_Click);
            // 
            // ListTopicos
            // 
            this.ListTopicos.FormattingEnabled = true;
            this.ListTopicos.Location = new System.Drawing.Point(12, 95);
            this.ListTopicos.Name = "ListTopicos";
            this.ListTopicos.Size = new System.Drawing.Size(164, 277);
            this.ListTopicos.TabIndex = 4;
            this.ListTopicos.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListTopicos_MouseDoubleClick);
            // 
            // Anbima4Teste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 387);
            this.Controls.Add(this.ListTopicos);
            this.Controls.Add(this.pgmButton1);
            this.Controls.Add(this.Grid);
            this.Name = "Anbima4Teste";
            this.Text = "Anbima4Teste";
            this.Load += new System.EventHandler(this.Anbima4Teste_Load);
            this.Controls.SetChildIndex(this.Grid, 0);
            this.Controls.SetChildIndex(this.pgmButton1, 0);
            this.Controls.SetChildIndex(this.ListTopicos, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.PgmControl.PgmDataGrid Grid;
        private Controls.PgmControl.PgmButton pgmButton1;
        private System.Windows.Forms.ListBox ListTopicos;
    }
}