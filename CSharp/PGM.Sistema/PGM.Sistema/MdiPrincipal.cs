using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PGM.WebService.Galgo;
using PGM.Controls;
using PGM.Controls.Sys;

namespace PGM.Sistema
{
    public partial class MdiPrincipal : frmPgmScreen
    {
        public MdiPrincipal()
        {
            MdiClient ctlMDI;
          
            // Loop through all of the form's controls looking
            // for the control of type MdiClient.
            foreach (Control ctl in this.Controls)
                if (ctl.GetType() == typeof(MdiClient))
                {
                    // Attempt to cast the control to type MdiClient.
                    ctlMDI = (MdiClient)ctl;

                    // Set the BackColor of the MdiClient control.
                    ctlMDI.BackColor = Color.White;

                    break;
                }

            InitializeComponent();

            PgmMainMenu oMenu = new PgmMainMenu();
            PgmMenuItem oI = oMenu.AddMenuSub("Importações");
            oI.AddMenu("Galgo", new GalgoConsultar(), this);
  
            this.Menu = oMenu;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GalgoConsultar oG = new GalgoConsultar();
            oG.MdiParent = this;
            oG.BringToFront();
            oG.Show();
        }

        private void MdiPrincipal_Load(object sender, EventArgs e)
        {

        }
    }
}
