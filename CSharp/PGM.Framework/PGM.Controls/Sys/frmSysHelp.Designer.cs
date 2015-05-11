namespace PGM.Controls.Sys
{
    partial class frmSysHelp
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cmdAlterar = new PGM.Controls.PgmControl.PgmButton();
            this.edtAjuda = new PGM.Controls.PgmControl.PgmEditBox();
            this.pgmLabel3 = new PGM.Controls.PgmControl.PgmLabel();
            this.pgmLabel2 = new PGM.Controls.PgmControl.PgmLabel();
            this.txtForm = new PGM.Controls.PgmControl.PgmTextBox();
            this.pgmLabel1 = new PGM.Controls.PgmControl.PgmLabel();
            this.txtName = new PGM.Controls.PgmControl.PgmTextBox();
            this.cmdFechar = new PGM.Controls.PgmControl.PgmButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(0, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(590, 362);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cmdAlterar);
            this.tabPage1.Controls.Add(this.edtAjuda);
            this.tabPage1.Controls.Add(this.pgmLabel3);
            this.tabPage1.Controls.Add(this.pgmLabel2);
            this.tabPage1.Controls.Add(this.txtForm);
            this.tabPage1.Controls.Add(this.pgmLabel1);
            this.tabPage1.Controls.Add(this.txtName);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(582, 336);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Informações";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cmdAlterar
            // 
            this.cmdAlterar.Location = new System.Drawing.Point(498, 41);
            this.cmdAlterar.Name = "cmdAlterar";
            this.cmdAlterar.Size = new System.Drawing.Size(75, 23);
            this.cmdAlterar.TabIndex = 39;
            this.cmdAlterar.Text = "Alterar";
            this.cmdAlterar.UseVisualStyleBackColor = true;
            this.cmdAlterar.Click += new System.EventHandler(this.cmdAlterar_Click);
            // 
            // edtAjuda
            // 
            this.edtAjuda.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edtAjuda.BackColor = System.Drawing.Color.White;
            this.edtAjuda.Enabled = false;
            this.edtAjuda.Location = new System.Drawing.Point(0, 78);
            this.edtAjuda.Multiline = true;
            this.edtAjuda.Name = "edtAjuda";
            this.edtAjuda.Obrigatory = false;
            this.edtAjuda.Size = new System.Drawing.Size(582, 258);
            this.edtAjuda.TabIndex = 38;
            // 
            // pgmLabel3
            // 
            this.pgmLabel3.AutoSize = true;
            this.pgmLabel3.Location = new System.Drawing.Point(3, 62);
            this.pgmLabel3.Name = "pgmLabel3";
            this.pgmLabel3.Size = new System.Drawing.Size(55, 13);
            this.pgmLabel3.TabIndex = 37;
            this.pgmLabel3.Text = "Descrição";
            // 
            // pgmLabel2
            // 
            this.pgmLabel2.AutoSize = true;
            this.pgmLabel2.Location = new System.Drawing.Point(9, 41);
            this.pgmLabel2.Name = "pgmLabel2";
            this.pgmLabel2.Size = new System.Drawing.Size(84, 13);
            this.pgmLabel2.TabIndex = 36;
            this.pgmLabel2.Text = "Nome da Classe";
            // 
            // txtForm
            // 
            this.txtForm.BackColor = System.Drawing.Color.White;
            this.txtForm.BlankIfZero = false;
            this.txtForm.DataType = PGM.Controls.PgmControl.PgmTextBox.Type.String;
            this.txtForm.Enabled = false;
            this.txtForm.Location = new System.Drawing.Point(104, 41);
            this.txtForm.Name = "txtForm";
            this.txtForm.Obrigatory = false;
            this.txtForm.Size = new System.Drawing.Size(377, 20);
            this.txtForm.TabIndex = 35;
            // 
            // pgmLabel1
            // 
            this.pgmLabel1.AutoSize = true;
            this.pgmLabel1.Location = new System.Drawing.Point(9, 15);
            this.pgmLabel1.Name = "pgmLabel1";
            this.pgmLabel1.Size = new System.Drawing.Size(74, 13);
            this.pgmLabel1.TabIndex = 34;
            this.pgmLabel1.Text = "Nome da Tela";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.BlankIfZero = false;
            this.txtName.DataType = PGM.Controls.PgmControl.PgmTextBox.Type.String;
            this.txtName.Enabled = false;
            this.txtName.Location = new System.Drawing.Point(104, 15);
            this.txtName.Name = "txtName";
            this.txtName.Obrigatory = false;
            this.txtName.Size = new System.Drawing.Size(377, 20);
            this.txtName.TabIndex = 33;
            // 
            // cmdFechar
            // 
            this.cmdFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdFechar.Location = new System.Drawing.Point(503, 369);
            this.cmdFechar.Name = "cmdFechar";
            this.cmdFechar.Size = new System.Drawing.Size(75, 23);
            this.cmdFechar.TabIndex = 31;
            this.cmdFechar.Text = "Fechar";
            this.cmdFechar.UseVisualStyleBackColor = true;
            // 
            // frmSysHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 403);
            this.Controls.Add(this.cmdFechar);
            this.Controls.Add(this.tabControl1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSysHelp";
            this.Opacity = 0.97D;
            this.Text = "Ajuda";
            this.Load += new System.EventHandler(this.FrmSysHelp_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmSysHelp_KeyPress);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private PgmControl.PgmButton cmdAlterar;
        private PgmControl.PgmEditBox edtAjuda;
        private PgmControl.PgmLabel pgmLabel3;
        private PgmControl.PgmLabel pgmLabel2;
        private PgmControl.PgmTextBox txtForm;
        private PgmControl.PgmLabel pgmLabel1;
        private PgmControl.PgmTextBox txtName;
        private PgmControl.PgmButton cmdFechar;

    }
}