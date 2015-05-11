using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PGM.Controls.Sys
{
    public partial class frmPgmScreen : Form
    {
        public frmPgmScreen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmSysLogin oF = new frmSysLogin();

            oF.MdiParent = this;
            oF.Show();
        }
    }
}
