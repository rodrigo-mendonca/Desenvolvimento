namespace PGMProcessing
{
    partial class Principal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Principal));
            this.pctScreen = new System.Windows.Forms.PictureBox();
            this.ToolMenu = new System.Windows.Forms.ToolStrip();
            this.ToolOpen = new System.Windows.Forms.ToolStripButton();
            this.ToolSave = new System.Windows.Forms.ToolStripSplitButton();
            this.ToolSalvar = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolSalvarComo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolUndo = new System.Windows.Forms.ToolStripButton();
            this.ToolRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolLeft = new System.Windows.Forms.ToolStripButton();
            this.ToolRight = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolErosaoMorfologica = new System.Windows.Forms.ToolStripButton();
            this.ToolDilatacaoMorfologica = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolHistograma = new System.Windows.Forms.ToolStripButton();
            this.ToolOpcoes = new System.Windows.Forms.ToolStripSplitButton();
            this.ToolColorReduction = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolFloydSteinberg = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolEqualizarCores = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolFiltroMedia = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolFiltroMediana = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolGaussiano = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolLaplaciano = new System.Windows.Forms.ToolStripMenuItem();
            this.stpReduction = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.stpMedia = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.stpDesvio = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pctScreen)).BeginInit();
            this.ToolMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stpReduction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stpMedia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stpDesvio)).BeginInit();
            this.SuspendLayout();
            // 
            // pctScreen
            // 
            this.pctScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pctScreen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctScreen.Location = new System.Drawing.Point(65, 35);
            this.pctScreen.Name = "pctScreen";
            this.pctScreen.Size = new System.Drawing.Size(524, 388);
            this.pctScreen.TabIndex = 1;
            this.pctScreen.TabStop = false;
            // 
            // ToolMenu
            // 
            this.ToolMenu.ImageScalingSize = new System.Drawing.Size(25, 25);
            this.ToolMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolOpen,
            this.ToolSave,
            this.toolStripSeparator1,
            this.ToolUndo,
            this.ToolRedo,
            this.toolStripSeparator2,
            this.ToolLeft,
            this.ToolRight,
            this.toolStripSeparator3,
            this.ToolErosaoMorfologica,
            this.ToolDilatacaoMorfologica,
            this.toolStripSeparator5,
            this.ToolHistograma,
            this.ToolOpcoes});
            this.ToolMenu.Location = new System.Drawing.Point(0, 0);
            this.ToolMenu.Name = "ToolMenu";
            this.ToolMenu.Size = new System.Drawing.Size(591, 32);
            this.ToolMenu.TabIndex = 2;
            this.ToolMenu.Text = "toolStrip1";
            // 
            // ToolOpen
            // 
            this.ToolOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolOpen.Image = ((System.Drawing.Image)(resources.GetObject("ToolOpen.Image")));
            this.ToolOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolOpen.Name = "ToolOpen";
            this.ToolOpen.Size = new System.Drawing.Size(29, 29);
            this.ToolOpen.Text = "Abrir";
            this.ToolOpen.Click += new System.EventHandler(this.ToolOpen_Click);
            // 
            // ToolSave
            // 
            this.ToolSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolSave.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolSalvar,
            this.ToolSalvarComo});
            this.ToolSave.Enabled = false;
            this.ToolSave.Image = ((System.Drawing.Image)(resources.GetObject("ToolSave.Image")));
            this.ToolSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolSave.Name = "ToolSave";
            this.ToolSave.Size = new System.Drawing.Size(41, 29);
            this.ToolSave.Text = "Salvar";
            this.ToolSave.ButtonClick += new System.EventHandler(this.ToolSave_ButtonClick);
            // 
            // ToolSalvar
            // 
            this.ToolSalvar.Enabled = false;
            this.ToolSalvar.Image = ((System.Drawing.Image)(resources.GetObject("ToolSalvar.Image")));
            this.ToolSalvar.Name = "ToolSalvar";
            this.ToolSalvar.Size = new System.Drawing.Size(161, 32);
            this.ToolSalvar.Text = "Salvar";
            this.ToolSalvar.Click += new System.EventHandler(this.ToolSavar_Click);
            // 
            // ToolSalvarComo
            // 
            this.ToolSalvarComo.Enabled = false;
            this.ToolSalvarComo.Image = ((System.Drawing.Image)(resources.GetObject("ToolSalvarComo.Image")));
            this.ToolSalvarComo.Name = "ToolSalvarComo";
            this.ToolSalvarComo.Size = new System.Drawing.Size(161, 32);
            this.ToolSalvarComo.Text = "Salvar Como...";
            this.ToolSalvarComo.Click += new System.EventHandler(this.ToolSavarComo_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // ToolUndo
            // 
            this.ToolUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolUndo.Enabled = false;
            this.ToolUndo.Image = ((System.Drawing.Image)(resources.GetObject("ToolUndo.Image")));
            this.ToolUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolUndo.Name = "ToolUndo";
            this.ToolUndo.Size = new System.Drawing.Size(29, 29);
            this.ToolUndo.Text = "Desfazer";
            this.ToolUndo.Click += new System.EventHandler(this.ToolUndo_Click);
            // 
            // ToolRedo
            // 
            this.ToolRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolRedo.Enabled = false;
            this.ToolRedo.Image = ((System.Drawing.Image)(resources.GetObject("ToolRedo.Image")));
            this.ToolRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolRedo.Name = "ToolRedo";
            this.ToolRedo.Size = new System.Drawing.Size(29, 29);
            this.ToolRedo.Text = "Refazer";
            this.ToolRedo.Click += new System.EventHandler(this.ToolRedo_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 32);
            // 
            // ToolLeft
            // 
            this.ToolLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolLeft.Enabled = false;
            this.ToolLeft.Image = ((System.Drawing.Image)(resources.GetObject("ToolLeft.Image")));
            this.ToolLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolLeft.Name = "ToolLeft";
            this.ToolLeft.Size = new System.Drawing.Size(29, 29);
            this.ToolLeft.Text = "Girar para esquerda";
            this.ToolLeft.Click += new System.EventHandler(this.ToolLeft_Click);
            // 
            // ToolRight
            // 
            this.ToolRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolRight.Enabled = false;
            this.ToolRight.Image = ((System.Drawing.Image)(resources.GetObject("ToolRight.Image")));
            this.ToolRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolRight.Name = "ToolRight";
            this.ToolRight.Size = new System.Drawing.Size(29, 29);
            this.ToolRight.Text = "Girar para direita";
            this.ToolRight.Click += new System.EventHandler(this.ToolRight_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 32);
            // 
            // ToolErosaoMorfologica
            // 
            this.ToolErosaoMorfologica.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolErosaoMorfologica.Enabled = false;
            this.ToolErosaoMorfologica.Image = ((System.Drawing.Image)(resources.GetObject("ToolErosaoMorfologica.Image")));
            this.ToolErosaoMorfologica.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolErosaoMorfologica.Name = "ToolErosaoMorfologica";
            this.ToolErosaoMorfologica.Size = new System.Drawing.Size(29, 29);
            this.ToolErosaoMorfologica.Text = "Erosão Morfológica";
            this.ToolErosaoMorfologica.Click += new System.EventHandler(this.ToolErosaoMorfologica_Click);
            // 
            // ToolDilatacaoMorfologica
            // 
            this.ToolDilatacaoMorfologica.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolDilatacaoMorfologica.Enabled = false;
            this.ToolDilatacaoMorfologica.Image = ((System.Drawing.Image)(resources.GetObject("ToolDilatacaoMorfologica.Image")));
            this.ToolDilatacaoMorfologica.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolDilatacaoMorfologica.Name = "ToolDilatacaoMorfologica";
            this.ToolDilatacaoMorfologica.Size = new System.Drawing.Size(29, 29);
            this.ToolDilatacaoMorfologica.Text = "Dilatação Morfológica";
            this.ToolDilatacaoMorfologica.Click += new System.EventHandler(this.ToolDilatacaoMorfologica_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 32);
            // 
            // ToolHistograma
            // 
            this.ToolHistograma.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolHistograma.Enabled = false;
            this.ToolHistograma.Image = ((System.Drawing.Image)(resources.GetObject("ToolHistograma.Image")));
            this.ToolHistograma.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolHistograma.Name = "ToolHistograma";
            this.ToolHistograma.Size = new System.Drawing.Size(29, 29);
            this.ToolHistograma.Text = "toolStripButton1";
            this.ToolHistograma.Click += new System.EventHandler(this.ToolHistograma_Click);
            // 
            // ToolOpcoes
            // 
            this.ToolOpcoes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolOpcoes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolColorReduction,
            this.ToolFloydSteinberg,
            this.ToolEqualizarCores,
            this.ToolFiltroMedia,
            this.ToolFiltroMediana,
            this.ToolGaussiano,
            this.ToolLaplaciano});
            this.ToolOpcoes.Enabled = false;
            this.ToolOpcoes.Image = ((System.Drawing.Image)(resources.GetObject("ToolOpcoes.Image")));
            this.ToolOpcoes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolOpcoes.Name = "ToolOpcoes";
            this.ToolOpcoes.Size = new System.Drawing.Size(41, 29);
            this.ToolOpcoes.Text = "toolStripSplitButton1";
            // 
            // ToolColorReduction
            // 
            this.ToolColorReduction.Name = "ToolColorReduction";
            this.ToolColorReduction.Size = new System.Drawing.Size(181, 22);
            this.ToolColorReduction.Text = "Reduzir Cores";
            this.ToolColorReduction.Click += new System.EventHandler(this.ToolColorReduction_Click);
            // 
            // ToolFloydSteinberg
            // 
            this.ToolFloydSteinberg.Name = "ToolFloydSteinberg";
            this.ToolFloydSteinberg.Size = new System.Drawing.Size(181, 22);
            this.ToolFloydSteinberg.Text = "Floyd Steinberg";
            this.ToolFloydSteinberg.Click += new System.EventHandler(this.ToolFloydSteinberg_Click);
            // 
            // ToolEqualizarCores
            // 
            this.ToolEqualizarCores.Name = "ToolEqualizarCores";
            this.ToolEqualizarCores.Size = new System.Drawing.Size(181, 22);
            this.ToolEqualizarCores.Text = "Equalizar Cores";
            this.ToolEqualizarCores.Click += new System.EventHandler(this.ToolEqualizarCores_Click);
            // 
            // ToolFiltroMedia
            // 
            this.ToolFiltroMedia.Name = "ToolFiltroMedia";
            this.ToolFiltroMedia.Size = new System.Drawing.Size(181, 22);
            this.ToolFiltroMedia.Text = "Filtro Média";
            this.ToolFiltroMedia.Click += new System.EventHandler(this.ToolFiltroMedia_Click);
            // 
            // ToolFiltroMediana
            // 
            this.ToolFiltroMediana.Name = "ToolFiltroMediana";
            this.ToolFiltroMediana.Size = new System.Drawing.Size(181, 22);
            this.ToolFiltroMediana.Text = "Filtro Mediana";
            this.ToolFiltroMediana.Click += new System.EventHandler(this.ToolFiltroMediana_Click);
            // 
            // ToolGaussiano
            // 
            this.ToolGaussiano.Name = "ToolGaussiano";
            this.ToolGaussiano.Size = new System.Drawing.Size(181, 22);
            this.ToolGaussiano.Text = "Operador Gaussiano";
            this.ToolGaussiano.Click += new System.EventHandler(this.ToolGaussiano_Click);
            // 
            // ToolLaplaciano
            // 
            this.ToolLaplaciano.Name = "ToolLaplaciano";
            this.ToolLaplaciano.Size = new System.Drawing.Size(181, 22);
            this.ToolLaplaciano.Text = "Laplaciano";
            this.ToolLaplaciano.Click += new System.EventHandler(this.ToolLaplaciano_Click);
            // 
            // stpReduction
            // 
            this.stpReduction.Enabled = false;
            this.stpReduction.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stpReduction.Location = new System.Drawing.Point(8, 51);
            this.stpReduction.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.stpReduction.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.stpReduction.Name = "stpReduction";
            this.stpReduction.Size = new System.Drawing.Size(51, 18);
            this.stpReduction.TabIndex = 3;
            this.stpReduction.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Nº Cores";
            // 
            // stpMedia
            // 
            this.stpMedia.Enabled = false;
            this.stpMedia.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stpMedia.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.stpMedia.Location = new System.Drawing.Point(8, 88);
            this.stpMedia.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.stpMedia.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.stpMedia.Name = "stpMedia";
            this.stpMedia.Size = new System.Drawing.Size(51, 18);
            this.stpMedia.TabIndex = 5;
            this.stpMedia.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.stpMedia.ValueChanged += new System.EventHandler(this.stpMedia_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Filtro";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Desvio";
            // 
            // stpDesvio
            // 
            this.stpDesvio.DecimalPlaces = 2;
            this.stpDesvio.Enabled = false;
            this.stpDesvio.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stpDesvio.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.stpDesvio.Location = new System.Drawing.Point(8, 125);
            this.stpDesvio.Name = "stpDesvio";
            this.stpDesvio.Size = new System.Drawing.Size(51, 18);
            this.stpDesvio.TabIndex = 7;
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 425);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.stpDesvio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.stpMedia);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stpReduction);
            this.Controls.Add(this.ToolMenu);
            this.Controls.Add(this.pctScreen);
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PGMProcessing";
            ((System.ComponentModel.ISupportInitialize)(this.pctScreen)).EndInit();
            this.ToolMenu.ResumeLayout(false);
            this.ToolMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stpReduction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stpMedia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stpDesvio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pctScreen;
        private System.Windows.Forms.ToolStrip ToolMenu;
        private System.Windows.Forms.ToolStripButton ToolOpen;
        private System.Windows.Forms.ToolStripButton ToolUndo;
        private System.Windows.Forms.ToolStripButton ToolRedo;
        private System.Windows.Forms.ToolStripButton ToolLeft;
        private System.Windows.Forms.ToolStripButton ToolRight;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSplitButton ToolSave;
        private System.Windows.Forms.ToolStripMenuItem ToolSalvar;
        private System.Windows.Forms.ToolStripMenuItem ToolSalvarComo;
        private System.Windows.Forms.NumericUpDown stpReduction;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripSplitButton ToolOpcoes;
        private System.Windows.Forms.ToolStripMenuItem ToolColorReduction;
        private System.Windows.Forms.ToolStripMenuItem ToolFloydSteinberg;
        private System.Windows.Forms.ToolStripMenuItem ToolEqualizarCores;
        private System.Windows.Forms.ToolStripMenuItem ToolFiltroMedia;
        private System.Windows.Forms.NumericUpDown stpMedia;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem ToolFiltroMediana;
        private System.Windows.Forms.ToolStripMenuItem ToolGaussiano;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown stpDesvio;
        private System.Windows.Forms.ToolStripMenuItem ToolLaplaciano;
        private System.Windows.Forms.ToolStripButton ToolErosaoMorfologica;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton ToolHistograma;
        private System.Windows.Forms.ToolStripButton ToolDilatacaoMorfologica;
    }
}

