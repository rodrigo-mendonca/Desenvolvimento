namespace InvestExcelAddin
{
    partial class menu : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public menu()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(menu));
            this.tabInvest = this.Factory.CreateRibbonTab();
            this.abaATIVO = this.Factory.CreateRibbonGroup();
            this.txtDATA = this.Factory.CreateRibbonEditBox();
            this.cboFundo = this.Factory.CreateRibbonDropDown();
            this.mnuCAMPOS = this.Factory.CreateRibbonMenu();
            this.chkCODATIVO = this.Factory.CreateRibbonCheckBox();
            this.chkNAMEATIVO = this.Factory.CreateRibbonCheckBox();
            this.chkPatriATIVO = this.Factory.CreateRibbonCheckBox();
            this.chkEMISSORATIVO = this.Factory.CreateRibbonCheckBox();
            this.chkQTDEATIVO = this.Factory.CreateRibbonCheckBox();
            this.chkCOTATIVO = this.Factory.CreateRibbonCheckBox();
            this.checkBox2 = this.Factory.CreateRibbonCheckBox();
            this.chkMOVATIVO = this.Factory.CreateRibbonCheckBox();
            this.chkVLMERCATIVO = this.Factory.CreateRibbonCheckBox();
            this.chkPRINCIPALATIVO = this.Factory.CreateRibbonCheckBox();
            this.cmdPATRIMONIO = this.Factory.CreateRibbonButton();
            this.tabInvest.SuspendLayout();
            this.abaATIVO.SuspendLayout();
            // 
            // tabInvest
            // 
            this.tabInvest.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tabInvest.Groups.Add(this.abaATIVO);
            this.tabInvest.Label = "Invest";
            this.tabInvest.Name = "tabInvest";
            // 
            // abaATIVO
            // 
            this.abaATIVO.Items.Add(this.txtDATA);
            this.abaATIVO.Items.Add(this.cboFundo);
            this.abaATIVO.Items.Add(this.mnuCAMPOS);
            this.abaATIVO.Items.Add(this.cmdPATRIMONIO);
            this.abaATIVO.Label = "Ativo";
            this.abaATIVO.Name = "abaATIVO";
            // 
            // txtDATA
            // 
            this.txtDATA.Label = "Data";
            this.txtDATA.MaxLength = 10;
            this.txtDATA.Name = "txtDATA";
            this.txtDATA.Text = null;
            // 
            // cboFundo
            // 
            this.cboFundo.Label = "Fundo";
            this.cboFundo.Name = "cboFundo";
            this.cboFundo.ItemsLoading += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.cboFundo_CarregaFundos);
            // 
            // mnuCAMPOS
            // 
            this.mnuCAMPOS.Dynamic = true;
            this.mnuCAMPOS.Items.Add(this.chkCODATIVO);
            this.mnuCAMPOS.Items.Add(this.chkNAMEATIVO);
            this.mnuCAMPOS.Items.Add(this.chkPatriATIVO);
            this.mnuCAMPOS.Items.Add(this.chkEMISSORATIVO);
            this.mnuCAMPOS.Items.Add(this.chkQTDEATIVO);
            this.mnuCAMPOS.Items.Add(this.chkCOTATIVO);
            this.mnuCAMPOS.Items.Add(this.checkBox2);
            this.mnuCAMPOS.Items.Add(this.chkMOVATIVO);
            this.mnuCAMPOS.Items.Add(this.chkVLMERCATIVO);
            this.mnuCAMPOS.Items.Add(this.chkPRINCIPALATIVO);
            this.mnuCAMPOS.Label = "Campos";
            this.mnuCAMPOS.Name = "mnuCAMPOS";
            this.mnuCAMPOS.Tag = "";
            // 
            // chkCODATIVO
            // 
            this.chkCODATIVO.Checked = true;
            this.chkCODATIVO.Label = "Código do ativo";
            this.chkCODATIVO.Name = "chkCODATIVO";
            this.chkCODATIVO.Tag = "CAR.FK_ATIVO";
            // 
            // chkNAMEATIVO
            // 
            this.chkNAMEATIVO.Checked = true;
            this.chkNAMEATIVO.Label = "Nome do ativo";
            this.chkNAMEATIVO.Name = "chkNAMEATIVO";
            this.chkNAMEATIVO.Tag = "ATI.DS_ATIVO";
            // 
            // chkPatriATIVO
            // 
            this.chkPatriATIVO.Checked = true;
            this.chkPatriATIVO.Label = "Valor de patrimonio";
            this.chkPatriATIVO.Name = "chkPatriATIVO";
            this.chkPatriATIVO.Tag = "CAR.VL_PATRIMONIO";
            // 
            // chkEMISSORATIVO
            // 
            this.chkEMISSORATIVO.Label = "Emissor";
            this.chkEMISSORATIVO.Name = "chkEMISSORATIVO";
            this.chkEMISSORATIVO.Tag = "INS.DS_INSTITUICAO AS DS_EMISSOR";
            // 
            // chkQTDEATIVO
            // 
            this.chkQTDEATIVO.Label = "Qtde.Ativo na carteira";
            this.chkQTDEATIVO.Name = "chkQTDEATIVO";
            this.chkQTDEATIVO.Tag = "CAR.QT_CARTEIRA";
            // 
            // chkCOTATIVO
            // 
            this.chkCOTATIVO.Label = "Cotação do ativo na carteira";
            this.chkCOTATIVO.Name = "chkCOTATIVO";
            this.chkCOTATIVO.Tag = "CAR.VL_COTACAO";
            // 
            // checkBox2
            // 
            this.checkBox2.Label = "Dividendo pago dos ativos ";
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Tag = "CAR.VL_AJUSTE1";
            // 
            // chkMOVATIVO
            // 
            this.chkMOVATIVO.Label = "Movimento do ativo na carteira";
            this.chkMOVATIVO.Name = "chkMOVATIVO";
            this.chkMOVATIVO.Tag = "CAR.VL_MOVIMENTOANT";
            // 
            // chkVLMERCATIVO
            // 
            this.chkVLMERCATIVO.Label = "Vl.de mercado";
            this.chkVLMERCATIVO.Name = "chkVLMERCATIVO";
            this.chkVLMERCATIVO.Tag = "CAR.VL_PRETOT";
            // 
            // chkPRINCIPALATIVO
            // 
            this.chkPRINCIPALATIVO.Label = "Valor Principal";
            this.chkPRINCIPALATIVO.Name = "chkPRINCIPALATIVO";
            this.chkPRINCIPALATIVO.Tag = "CAR.VL_PRINCIPAL";
            // 
            // cmdPATRIMONIO
            // 
            this.cmdPATRIMONIO.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.cmdPATRIMONIO.Image = ((System.Drawing.Image)(resources.GetObject("cmdPATRIMONIO.Image")));
            this.cmdPATRIMONIO.Label = "Buscar";
            this.cmdPATRIMONIO.Name = "cmdPATRIMONIO";
            this.cmdPATRIMONIO.ShowImage = true;
            this.cmdPATRIMONIO.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.cmdPATRIMONIO_Click);
            // 
            // menu
            // 
            this.Name = "menu";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tabInvest);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Menu_Load);
            this.tabInvest.ResumeLayout(false);
            this.tabInvest.PerformLayout();
            this.abaATIVO.ResumeLayout(false);
            this.abaATIVO.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tabInvest;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup abaATIVO;
        internal Microsoft.Office.Tools.Ribbon.RibbonEditBox txtDATA;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown cboFundo;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu mnuCAMPOS;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox chkCODATIVO;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox chkNAMEATIVO;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox chkPatriATIVO;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox chkEMISSORATIVO;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox chkQTDEATIVO;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox chkCOTATIVO;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox checkBox2;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox chkMOVATIVO;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox chkVLMERCATIVO;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox chkPRINCIPALATIVO;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton cmdPATRIMONIO;
    }

    partial class ThisRibbonCollection
    {
        internal menu Ribbon1
        {
            get { return this.GetRibbon<menu>(); }
        }
    }
}
