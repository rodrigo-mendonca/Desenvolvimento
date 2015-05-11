using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.Data;
using InvestAddinCore;

namespace InvestExcelAddin
{
    public partial class menu
    {
        private void Menu_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void cmdPATRIMONIO_Click(object sender, RibbonControlEventArgs e)
        {
            // VERIFICA DATA
            DateTime result;
            if (DateTime.TryParse(txtDATA.Text, out result) == false)
            {
                txtDATA.Text = "";
                return;
            }

            if (cboFundo.Items.Count == 0)
            {
                return;
            }

            ThisAddIn.PreenchePlanilha(txtDATA.Text, cboFundo.SelectedItem.Label, mnuCAMPOS);
        }

        private void cboFundo_CarregaFundos(object sender, RibbonControlEventArgs e)
        {
            Core.PreencheFundos(cboFundo,this.Factory);
        }
    }
}
