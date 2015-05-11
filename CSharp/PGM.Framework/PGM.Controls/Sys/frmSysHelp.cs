using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PGM.Interfaces;
using PGM.Sys;
using PGM.SQL.Models;

namespace PGM.Controls.Sys
{
    public partial class frmSysHelp :Form
    {
        #region Propriedades
        IControllerDigitar<AjudaSistema> oControl;
        ISysRepository<AjudaSistema> oRepo;
        AjudaSistema oEE = new AjudaSistema();
        #endregion

        #region Construtores
        public frmSysHelp()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos
        private void FrmSysHelp_Load(object sender, EventArgs e)
        {
            // pega o form que chamou a tela
            Form oParent = this.Owner;

            // cria um repositorio e muda a conexão
            oRepo = PgmInjector.GetInstance<ISysRepository<AjudaSistema>>();
            oRepo.SetConnection(PgmGlobal.DbSysConnectName);

            // procura na base de dados se existe essa tela cadastrada
            IList<AjudaSistema> oList = oRepo.Where(i => i.Form == oParent.Name && i.Campo == "");

            // se encontrar, preenche o EE para exibir na tela
            if (oList.Count != 0)
                oEE = oList[0];
            else
            {
                // se não encontrar, preenche com os dados padrões
                oEE.Name    = oParent.Text;
                oEE.Form    = oParent.GetType().FullName;
                oEE.Sistema = "Net";
                oEE.Campo   = "";
                oEE.Parent  = "";
            }
            // cria um controlador de formdig para controlar de forma mais facil o que está na tela
            oControl = PgmInjector.GetInstance<IControllerDigitar<AjudaSistema>>();
            oControl.SetConnection(PgmGlobal.DbSysConnectName); // altera para a base de estrutura
            
            // registra os controles com as propriedade do modelo
            oControl.Register(i => i.Name, txtName)
                    .Register(i => i.Form, txtForm)
                    .Register(i => i.Ajuda, edtAjuda);
            // preenche os controles
            oControl.SetEE(oEE);
        }

        private void cmdFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdAlterar_Click(object sender, EventArgs e)
        {
            bool Enabled = false;
            if (cmdAlterar.Text != "Salvar")
            {
                Enabled = !Enabled;
                cmdAlterar.Text = "Salvar";
            }
            else
            {
                // pega o modelo preenche do controle
                oEE = oControl.GetEE();
                oRepo.Insert(oEE); // insere no banco
                // altera o status do botão
                cmdAlterar.Text = "Alterar";
            }

            txtName.Enabled = Enabled;
            edtAjuda.Enabled = Enabled;
        }

        private void frmSysHelp_KeyPress(object sender, KeyPressEventArgs e)
        {
            // se for ESC fecha a tela
            if (e.KeyChar == 27)
                this.Close();
        }
        #endregion
    }
}
