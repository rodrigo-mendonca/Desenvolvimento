using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using Microsoft.Office.Interop.Excel;

namespace InitInstall
{
    [RunInstaller(true)]
    public partial class InitInstall : System.Configuration.Install.Installer
    {
        public InitInstall()
        {
            InitializeComponent();
        }
        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Commit(IDictionary savedState)
        {
            string cDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string cXll = cDir+@"\InvestExcel.xll";

            var oExcel = new Microsoft.Office.Interop.Excel.Application();
            var workbook = oExcel.Workbooks.Add(Type.Missing);
            oExcel.AddIns.Add(cXll, false).Installed = true;
            
            //oExcel.Quit();
            oExcel.Visible = true;
            oExcel = null;
            base.Commit(savedState);
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
        }
    }
}
