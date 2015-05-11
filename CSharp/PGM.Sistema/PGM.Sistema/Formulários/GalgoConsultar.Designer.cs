namespace Anbima.WCFSamples
{
    partial class GalgoConsultar
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
            this.cmdExportar = new PGM.Controls.PgmControl.PgmButton();
            this.cmdConsulta = new PGM.Controls.PgmControl.PgmButton();
            this.label1 = new PGM.Controls.PgmControl.PgmLabel();
            this.label2 = new PGM.Controls.PgmControl.PgmLabel();
            this.cmdImportar = new PGM.Controls.PgmControl.PgmButton();
            this.cmdFechar = new PGM.Controls.PgmControl.PgmButton();
            this.cmdImportarGalgo = new PGM.Controls.PgmControl.PgmButton();
            this.dtpDe = new PGM.Controls.PgmControl.PgmDateTimePicker();
            this.dtpAte = new PGM.Controls.PgmControl.PgmDateTimePicker();
            this.cmdConfig = new PGM.Controls.PgmControl.PgmButton();
            this.SuspendLayout();
            // 
            // Grid
            // 
            this.Grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Grid.IsSortable = true;
            this.Grid.Location = new System.Drawing.Point(-2, 84);
            this.Grid.Name = "Grid";
            this.Grid.Size = new System.Drawing.Size(505, 242);
            this.Grid.TabIndex = 5;
            // 
            // cmdExportar
            // 
            this.cmdExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExportar.Location = new System.Drawing.Point(403, 33);
            this.cmdExportar.Name = "cmdExportar";
            this.cmdExportar.Size = new System.Drawing.Size(93, 23);
            this.cmdExportar.TabIndex = 3;
            this.cmdExportar.Text = "Exportar XML";
            this.cmdExportar.UseVisualStyleBackColor = true;
            this.cmdExportar.Click += new System.EventHandler(this.XML_Click);
            // 
            // cmdConsulta
            // 
            this.cmdConsulta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConsulta.Location = new System.Drawing.Point(403, 62);
            this.cmdConsulta.Name = "cmdConsulta";
            this.cmdConsulta.Size = new System.Drawing.Size(93, 23);
            this.cmdConsulta.TabIndex = 4;
            this.cmdConsulta.Text = "Consultar";
            this.cmdConsulta.UseVisualStyleBackColor = true;
            this.cmdConsulta.Click += new System.EventHandler(this.pgmButton1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "De:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Ate:";
            // 
            // cmdImportar
            // 
            this.cmdImportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdImportar.Location = new System.Drawing.Point(303, 33);
            this.cmdImportar.Name = "cmdImportar";
            this.cmdImportar.Size = new System.Drawing.Size(94, 23);
            this.cmdImportar.TabIndex = 2;
            this.cmdImportar.Text = "Importar XML";
            this.cmdImportar.UseVisualStyleBackColor = true;
            this.cmdImportar.Click += new System.EventHandler(this.cmdImportar_Click);
            // 
            // cmdFechar
            // 
            this.cmdFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdFechar.Location = new System.Drawing.Point(421, 332);
            this.cmdFechar.Name = "cmdFechar";
            this.cmdFechar.Size = new System.Drawing.Size(75, 23);
            this.cmdFechar.TabIndex = 8;
            this.cmdFechar.Text = "Fechar";
            this.cmdFechar.UseVisualStyleBackColor = true;
            this.cmdFechar.Click += new System.EventHandler(this.cmdFechar_Click);
            // 
            // cmdImportarGalgo
            // 
            this.cmdImportarGalgo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdImportarGalgo.Location = new System.Drawing.Point(12, 332);
            this.cmdImportarGalgo.Name = "cmdImportarGalgo";
            this.cmdImportarGalgo.Size = new System.Drawing.Size(75, 23);
            this.cmdImportarGalgo.TabIndex = 6;
            this.cmdImportarGalgo.Text = "Importar";
            this.cmdImportarGalgo.UseVisualStyleBackColor = true;
            this.cmdImportarGalgo.Click += new System.EventHandler(this.cmdImportarGalgo_Click);
            // 
            // dtpDe
            // 
            this.dtpDe.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpDe.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDe.Location = new System.Drawing.Point(42, 35);
            this.dtpDe.Name = "dtpDe";
            this.dtpDe.Size = new System.Drawing.Size(141, 20);
            this.dtpDe.TabIndex = 0;
            // 
            // dtpAte
            // 
            this.dtpAte.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpAte.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpAte.Location = new System.Drawing.Point(42, 58);
            this.dtpAte.Name = "dtpAte";
            this.dtpAte.Size = new System.Drawing.Size(141, 20);
            this.dtpAte.TabIndex = 1;
            // 
            // cmdConfig
            // 
            this.cmdConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConfig.Enabled = false;
            this.cmdConfig.Location = new System.Drawing.Point(340, 332);
            this.cmdConfig.Name = "cmdConfig";
            this.cmdConfig.Size = new System.Drawing.Size(75, 23);
            this.cmdConfig.TabIndex = 7;
            this.cmdConfig.Text = "Configurar";
            this.cmdConfig.UseVisualStyleBackColor = true;
            this.cmdConfig.Click += new System.EventHandler(this.cmdConfig_Click);
            // 
            // GalgoConsultar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 367);
            this.Controls.Add(this.cmdConfig);
            this.Controls.Add(this.dtpAte);
            this.Controls.Add(this.dtpDe);
            this.Controls.Add(this.cmdImportarGalgo);
            this.Controls.Add(this.cmdFechar);
            this.Controls.Add(this.cmdImportar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdConsulta);
            this.Controls.Add(this.cmdExportar);
            this.Controls.Add(this.Grid);
            this.Name = "GalgoConsultar";
            this.Text = "Galgo Importar ";
            this.PgmLoadingTimer += new System.EventHandler(this.GalgoConsultar_PgmLoadingTimer);
            this.Load += new System.EventHandler(this.TesteConsulta_Load);
            this.Controls.SetChildIndex(this.Grid, 0);
            this.Controls.SetChildIndex(this.cmdExportar, 0);
            this.Controls.SetChildIndex(this.cmdConsulta, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.cmdImportar, 0);
            this.Controls.SetChildIndex(this.cmdFechar, 0);
            this.Controls.SetChildIndex(this.cmdImportarGalgo, 0);
            this.Controls.SetChildIndex(this.dtpDe, 0);
            this.Controls.SetChildIndex(this.dtpAte, 0);
            this.Controls.SetChildIndex(this.cmdConfig, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PGM.Controls.PgmControl.PgmDataGrid Grid;
        private PGM.Controls.PgmControl.PgmButton cmdExportar;
        private PGM.Controls.PgmControl.PgmButton cmdConsulta;
        private PGM.Controls.PgmControl.PgmLabel label1;
        private PGM.Controls.PgmControl.PgmLabel label2;
        private PGM.Controls.PgmControl.PgmButton cmdImportar;
        private PGM.Controls.PgmControl.PgmButton cmdFechar;
        private PGM.Controls.PgmControl.PgmButton cmdImportarGalgo;
        private PGM.Controls.PgmControl.PgmDateTimePicker dtpDe;
        private PGM.Controls.PgmControl.PgmDateTimePicker dtpAte;
        private PGM.Controls.PgmControl.PgmButton cmdConfig;
    }
}