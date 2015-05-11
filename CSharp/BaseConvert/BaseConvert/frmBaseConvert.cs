using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BaseConvert
{
    public partial class frmBaseConvert : Form
    {
        public Conversor cConvert = new Conversor("");

        public frmBaseConvert()
        {
            InitializeComponent();

            string[] cTipos = cConvert.GetTipos();

            for (int i = 0; i < cTipos.Length; i++)
            {
                cboLIST1.Items.Add(cTipos[i]);
                cboLIST2.Items.Add(cTipos[i]);
            }
            cboLIST1.SelectedIndex = 0;
            cboLIST2.SelectedIndex = 0;
        }

        private void cmdCONVERT_Click(object sender, EventArgs e)
        {
            cConvert.cIn        = txtIN.Text;
            cConvert.lInvert    = chkREVERT.Checked;
            cConvert.cBaseIn    = cboLIST1.Text;
            cConvert.cBaseOut   = cboLIST2.Text;

            cConvert.Converter();
            txtOUT.Text = cConvert.cOut;

        }

        private void lblPARA_Click(object sender, EventArgs e)
        {
            int nIndex = cboLIST2.SelectedIndex;

            cboLIST2.SelectedIndex = cboLIST1.SelectedIndex;
            cboLIST1.SelectedIndex = nIndex;
        }
    }
}
