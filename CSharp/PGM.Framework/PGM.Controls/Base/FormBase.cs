using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PGM.Common;
using PGM.Controls.Sys;
using System.Threading;

namespace PGM.Controls
{
    public partial class FormBase : Form
    {
        #region Propriedades

        /// <summary>
        /// Lista de Paramestros passados para o Form.
        /// </summary>
        public object[] ListParams { get; set; }
        /// <summary>
        /// Indica se o form tem que fechar ao aperta o ESC.
        /// </summary>
        [Description("Indica se o form tem que fechar ao aperta o ESC.")]
        public bool CloseEsc { get; set; }

        public Form oFormParent;
        Thread oThreadLoad;
        #endregion

        #region Construtores
        public FormBase()
        {
            InitializeComponent();
        }
        #endregion

        #region Metodos

        /// <summary>
        /// Este metodo tem como função executar um metodo como thread e exibir para o usuário uma indicação que está carregando.
        /// </summary>
        /// <param name="tRow">Objeto da Tabela</param>
        public void RunLoading(ThreadStart tThreadStart)
        {
            pctLoading.Visible = true;
            Timer.Enabled = true;
            oThreadLoad = new Thread(tThreadStart);
            oThreadLoad.Start();
        }

        /// <summary>
        /// Este metodo tem como função indicar se a thread do loading terminou
        /// </summary>
        /// <returns>Retorna True ou False para informar se a thread terminou.</returns>
        public bool FinishLoading()
        {
            if (oThreadLoad != null)
            {
                // se a thread estiver terminado, exibe os dados
                if (oThreadLoad.ThreadState == ThreadState.Stopped)
                {
                    pctLoading.Visible = false;
                    Timer.Enabled = false;
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Eventos
        private void FormBase_Resize(object sender, EventArgs e)
        {
            // Força o ajuda a sempre ficar no top e canto esquerdo
            this.lblinkAjuda.Top = 2;
            this.lblinkAjuda.Left = this.Width - 55;
            this.TopPanel.Width = this.Width;
        }

        /// <summary>
        /// Evento executa para cada fez que o Timer do loading executar
        /// </summary>
        public event EventHandler PgmLoadingTimer;
        public virtual void DoPgmLoadingTimer(object o, EventArgs e) { if (PgmLoadingTimer != null)  PgmLoadingTimer(o, e); }

        /// <summary>
        /// Evento executa após as configurações basicas
        /// </summary>
        public event EventHandler PgmInit;
        public virtual void DoPgmInit(object o, EventArgs e) { if (PgmInit != null)  PgmInit(o, e); }

        private void FormBase_Load(object sender, EventArgs e)
        {
            DoPgmInit(sender, e);
        }
        private void lblinkAjuda_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmSysHelp Form = new frmSysHelp();
            Form.ShowDialog(this);
        }
        private void FormBase_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27 && CloseEsc)
                this.Close();
        }
        #endregion

        private void Timer_Tick(object sender, EventArgs e)
        {
            DoPgmLoadingTimer(sender, e);
        }
    }
}
