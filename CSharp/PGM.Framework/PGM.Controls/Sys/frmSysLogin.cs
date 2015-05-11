using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using PGM.Sys;
using PGM.Controllers;
using PGM.Interfaces;

namespace PGM.Controls.Sys
{
    public partial class frmSysLogin : FormBase
    {
        #region Propriedades
        /// <summary>
        /// Indica se conseguiu fazer o login
        /// </summary>
        public bool LoginSucess = false;
        
        private ControllerLogin Controller = (ControllerLogin)PgmInjector.GetInstance<IControllerLogin>();
        private PgmConnection oConexao;
        #endregion

        #region Construtores
        public frmSysLogin()
        {
            InitializeComponent();
        }
        #endregion

        #region Metodos
        public void RunLogin()
        {
            Controller.OpenConnection(oConexao);
            LoginSucess = Controller.ValidUser(txtLogin.Text, txtSenha.Text.ToUpper());
        }
        #endregion

        #region Eventos

        private void frmSysLogin_Load(object sender, EventArgs e)
        {
            // busca o nome do usuario conectado na maquina local
            txtLogin.Text = System.Environment.UserName.ToUpper();

            foreach (Control oItem in this.Controls)
            {
                oItem.KeyPress += frmSysLogin_KeyPress;
            }

            cboConexao.DataSource    = PgmGlobal.ListConnections();
            cboConexao.DisplayMember = "ConnectionDisplayName";
            cboConexao.ValueMember   = "ConnectionName";
            // sempre inicia focado na senha
            txtSenha.Select();
        }

        private void cmdCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSysLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            // se precionar enter chama o Ok
            if (e.KeyChar == 13)
                cmdOk_Click(sender, e);
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            lblMsgErro.Visible = false;

            if (txtLogin.IsEmpty())
            {
                lblMsgErro.Text = "Digite o nome do usuário!";
                lblMsgErro.Visible = true;
                return;
            }

            if (txtSenha.IsEmpty())
            {
                lblMsgErro.Text = "Digite a senha do usuário!";
                lblMsgErro.Visible = true;
                return;
            }
            cmdOk.Enabled = false;
            oConexao = (PgmConnection)cboConexao.SelectedItem;
            RunLoading(new ThreadStart(RunLogin));
        }

        private void frmSysLogin_PgmLoadingTimer(object sender, EventArgs e)
        {
            if (FinishLoading())
            {
                if (!LoginSucess)
                {
                    lblMsgErro.Text = "Nome de usuário ou senha inválidos!";
                    lblMsgErro.Visible = true;
                    timer.Enabled = false;
                    cmdOk.Enabled = true;
                }
                else
                    this.Close();
            }
        }
        #endregion
    }
}
