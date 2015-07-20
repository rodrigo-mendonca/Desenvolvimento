using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using USBBackup;

namespace USBBackupWindows
{
    public partial class USBBackupWindows : Form
    {
        USBBackup.USBBackup Controle = new USBBackup.USBBackup();

        public USBBackupWindows()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DriversList.DataSource = Controle.ListDrives();
        }

        private void DriversList_DoubleClick(object sender, EventArgs e)
        {
            string DriveName = (string)DriversList.SelectedValue;

            DriveInfo Dri = DriveInfo.GetDrives().Where(i=>i.Name == DriveName).Single();
            DirectorysList.DataSource = Controle.ListDirectory(Dri);
        }
    }
}
