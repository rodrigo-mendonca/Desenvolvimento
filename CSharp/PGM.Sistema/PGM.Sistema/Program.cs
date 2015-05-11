using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using PGM.Sys;
using PGM.Modules;
using PGM.Controls.Sys;

namespace PGM.Sistema
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            PgmModulo oMod = new PgmModulo();
            oMod.SetBinding(PgmInjector.SjContainer);

            frmSysLogin oLogin = new frmSysLogin();
            PgmGlobal.Run(oLogin);

            if (oLogin.LoginSucess)
            {
                PgmGlobal.Run(new MdiPrincipal());
            }
        }
    }
}
