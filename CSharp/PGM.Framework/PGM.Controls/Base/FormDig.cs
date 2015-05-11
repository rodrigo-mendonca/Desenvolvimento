using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.Threading;

using SimpleInjector;

using PGM.Controls;
using PGM.Controls.PgmControl;
using PGM.SQL.Repositories;
using PGM.SQL.Models;
using PGM.Interfaces;
using PGM.Controllers;
using PGM.Sys;

namespace PGM.Controls
{
    

    public partial class FormDig : FormBase
    {
        #region Propriedades
        public IControllerDigitar oControl;

        #endregion

        # region Construtores
        public FormDig()
        {
            InitializeComponent();
        }
        #endregion

        #region Metodos

        /// <summary>
        /// Define qual é o modelo para o controlador
        /// </summary>
        ///<returns>Retorna o controlador</returns>
        protected IControllerDigitar<T> SetModel<T>() where T : IBase // busca imlentação da controleer com tipo passado
        {
            oControl = PgmInjector.GetInstance<IControllerDigitar<T>>();
            lblLinkID.Text = oControl.cLabel;
            return (IControllerDigitar<T>)oControl;
        }

        /// <summary>
        /// Define qual é o conexão para o controlador
        /// </summary>
        ///<returns>Retorna o controlador</returns>
        protected void SetConnection(string tNameOrConnectionString)
        {
            oControl.SetConnection(tNameOrConnectionString);
        }

        /// <summary>
        /// Retorna o modelo
        /// </summary>
        ///<returns>Retorna modelo da criação</returns>
        public T GetModelEE<T>() where T : IBase
        {
            return ((IControllerDigitar<T>)oControl).GetEE();
        }
        /// <summary>
        /// Adicina o modelo para os controles registrados
        /// </summary>
        /// <param name="tModel">Modelo para preenche os controles</param>
        ///<returns>Retorna o controlador</returns>
        public IControllerDigitar<T> SetModelEE<T>(T tModel) where T : IBase
        {
            var oCon = ((IControllerDigitar<T>)oControl);
            oCon.SetEE(tModel);

            return oCon;
        }

        private void cmdCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Eventos
        /// <summary>
        /// Evento tem que ser usado para registrar os campos no modelo
        /// </summary>
        public event EventHandler PgmRegister;
        public virtual void DoPgmRegister(object o, EventArgs e) { if (PgmRegister != null)  PgmRegister(o, e); }

        /// <summary>
        /// Evento executa antes de salvar os dados
        /// </summary>
        public event EventHandler PgmBefore;
        public virtual void DoPgmBefore(object o, EventArgs e) { if (PgmBefore != null)  PgmBefore(o, e); }
        /// <summary>
        /// Evento executa depois de salvar os dados
        /// </summary>
        public event EventHandler PgmAfter;
        public virtual void DoPgmAfter(object o, EventArgs e) { if (PgmAfter != null)  PgmAfter(o, e); }

        private void FormDig_Load(object sender, EventArgs e)
        {
            // não precisa executar o load quando estiver no modo Design
            if (this.DesignMode)
                return;

            DoPgmRegister(sender,e);

            if (this.ListParams[0] != null)
            {
                if (this.ListParams[0].GetType() == typeof(PgmDataGrid))
                {
                    PgmDataGrid oGrid = (PgmDataGrid)this.ListParams[0];
                     oControl.SetId(oGrid.GetSelectRowColumn("PkId"));
                }
                else
                {
                    oControl.SetId(this.ListParams[0]);
                }
            }

            lblLinkID.Text = oControl.cLabel;
            DoPgmInit(sender, e);
        }

        private void FormDig_Resize(object sender, EventArgs e)
        {
            // Ajusta os controles para sempre ficarem no mesmo lugar para diferentes tamanho de tela
            BottonPanel.Top = this.Height - (BottonPanel.Height * 2) - 2;
            BottonPanel.Left = -2;
            BottonPanel.Width = this.Width + 2;
        }
        private void cmdOk_Click(object sender, EventArgs e)
        {
            DoPgmBefore(null, null);

            cmdOk.Enabled = false;
            RunLoading(new ThreadStart(SaveChances));
        }

        private void SaveChances()
        {
            oControl.SaveChances();
        }
        #endregion

        private void FormDig_PgmLoadingTimer(object sender, EventArgs e)
        {
            if (FinishLoading())
            {
                cmdOk.Enabled = true;
                DoPgmAfter(null, null);
                // se existe um form anterior, chama o ambientar para recarregar a grid
                if (this.oFormParent != null)
                {
                    FormConsult oForm = (FormConsult)this.oFormParent;
                    oForm.DoPgmAmbient(sender, e);
                }
                this.Close();
            }
        }
    }
}