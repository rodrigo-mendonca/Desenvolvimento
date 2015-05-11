namespace PGM.Controls.Sys
{
    partial class frmSysLogin
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
            this.txtLogin = new PGM.Controls.PgmControl.PgmTextBox();
            this.cmdOk = new PGM.Controls.PgmControl.PgmButton();
            this.cmdCancelar = new PGM.Controls.PgmControl.PgmButton();
            this.pgmLabel1 = new PGM.Controls.PgmControl.PgmLabel();
            this.pgmLabel2 = new PGM.Controls.PgmControl.PgmLabel();
            this.txtSenha = new PGM.Controls.PgmControl.PgmTextBox();
            this.cboConexao = new PGM.Controls.PgmControl.PgmComboBox();
            this.pctLogo = new System.Windows.Forms.PictureBox();
            this.lblMsgErro = new PGM.Controls.PgmControl.PgmLabel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pctLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLogin
            // 
            this.txtLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(225)))));
            this.txtLogin.BlankIfZero = false;
            this.txtLogin.DataType = PGM.Controls.PgmControl.PgmTextBox.Type.String;
            this.txtLogin.Location = new System.Drawing.Point(284, 35);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Obrigatory = true;
            this.txtLogin.Size = new System.Drawing.Size(146, 20);
            this.txtLogin.TabIndex = 0;
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(235, 111);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(75, 23);
            this.cmdOk.TabIndex = 2;
            this.cmdOk.Text = "Ok";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdCancelar
            // 
            this.cmdCancelar.Location = new System.Drawing.Point(355, 111);
            this.cmdCancelar.Name = "cmdCancelar";
            this.cmdCancelar.Size = new System.Drawing.Size(75, 23);
            this.cmdCancelar.TabIndex = 3;
            this.cmdCancelar.Text = "Cancelar";
            this.cmdCancelar.UseVisualStyleBackColor = true;
            this.cmdCancelar.Click += new System.EventHandler(this.cmdCancelar_Click);
            // 
            // pgmLabel1
            // 
            this.pgmLabel1.AutoSize = true;
            this.pgmLabel1.Location = new System.Drawing.Point(232, 38);
            this.pgmLabel1.Name = "pgmLabel1";
            this.pgmLabel1.Size = new System.Drawing.Size(46, 13);
            this.pgmLabel1.TabIndex = 5;
            this.pgmLabel1.Text = "Usuário:";
            // 
            // pgmLabel2
            // 
            this.pgmLabel2.AutoSize = true;
            this.pgmLabel2.Location = new System.Drawing.Point(232, 64);
            this.pgmLabel2.Name = "pgmLabel2";
            this.pgmLabel2.Size = new System.Drawing.Size(41, 13);
            this.pgmLabel2.TabIndex = 7;
            this.pgmLabel2.Text = "Senha:";
            // 
            // txtSenha
            // 
            this.txtSenha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(225)))));
            this.txtSenha.BlankIfZero = false;
            this.txtSenha.DataType = PGM.Controls.PgmControl.PgmTextBox.Type.String;
            this.txtSenha.Location = new System.Drawing.Point(284, 61);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.Obrigatory = true;
            this.txtSenha.PasswordChar = '*';
            this.txtSenha.Size = new System.Drawing.Size(146, 20);
            this.txtSenha.TabIndex = 1;
            // 
            // cboConexao
            // 
            this.cboConexao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConexao.FormattingEnabled = true;
            this.cboConexao.Location = new System.Drawing.Point(12, 115);
            this.cboConexao.Name = "cboConexao";
            this.cboConexao.Size = new System.Drawing.Size(198, 21);
            this.cboConexao.TabIndex = 4;
            // 
            // pctLogo
            // 
            this.pctLogo.Image = global::PGM.Controls.Properties.Resources.logo;
            this.pctLogo.Location = new System.Drawing.Point(12, 26);
            this.pctLogo.Name = "pctLogo";
            this.pctLogo.Size = new System.Drawing.Size(198, 82);
            this.pctLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctLogo.TabIndex = 9;
            this.pctLogo.TabStop = false;
            // 
            // lblMsgErro
            // 
            this.lblMsgErro.AutoSize = true;
            this.lblMsgErro.ForeColor = System.Drawing.Color.Red;
            this.lblMsgErro.Location = new System.Drawing.Point(235, 81);
            this.lblMsgErro.Name = "lblMsgErro";
            this.lblMsgErro.Size = new System.Drawing.Size(0, 13);
            this.lblMsgErro.TabIndex = 51;
            this.lblMsgErro.Visible = false;
            // 
            // frmSysLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 146);
            this.Controls.Add(this.lblMsgErro);
            this.Controls.Add(this.pctLogo);
            this.Controls.Add(this.cboConexao);
            this.Controls.Add(this.pgmLabel2);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.pgmLabel1);
            this.Controls.Add(this.cmdCancelar);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.txtLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSysLogin";
            this.Opacity = 0.97D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.PgmLoadingTimer += new System.EventHandler(this.frmSysLogin_PgmLoadingTimer);
            this.Load += new System.EventHandler(this.frmSysLogin_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmSysLogin_KeyPress);
            this.Controls.SetChildIndex(this.txtLogin, 0);
            this.Controls.SetChildIndex(this.cmdOk, 0);
            this.Controls.SetChildIndex(this.cmdCancelar, 0);
            this.Controls.SetChildIndex(this.pgmLabel1, 0);
            this.Controls.SetChildIndex(this.txtSenha, 0);
            this.Controls.SetChildIndex(this.pgmLabel2, 0);
            this.Controls.SetChildIndex(this.cboConexao, 0);
            this.Controls.SetChildIndex(this.pctLogo, 0);
            this.Controls.SetChildIndex(this.lblMsgErro, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pctLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PgmControl.PgmTextBox txtLogin;
        private PgmControl.PgmButton cmdOk;
        private PgmControl.PgmButton cmdCancelar;
        private PgmControl.PgmLabel pgmLabel1;
        private PgmControl.PgmLabel pgmLabel2;
        private PgmControl.PgmTextBox txtSenha;
        private PgmControl.PgmComboBox cboConexao;
        private System.Windows.Forms.PictureBox pctLogo;
        private PgmControl.PgmLabel lblMsgErro;
        private System.Windows.Forms.Timer timer;
    }
}