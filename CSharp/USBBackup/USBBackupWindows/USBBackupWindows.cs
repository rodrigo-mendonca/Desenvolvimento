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

        private void cmdOri_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog Brower = new FolderBrowserDialog();
            Brower.ShowDialog();

            DirectoryInfo Dir = new DirectoryInfo(Brower.SelectedPath);
            txtOrigen.Text = Brower.SelectedPath;
            Controle.SetOrigin(Dir);
        }

        private void cmdDes_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog Brower = new FolderBrowserDialog();
            Brower.ShowDialog();

            DirectoryInfo Dir = new DirectoryInfo(Brower.SelectedPath);
            txtDestino.Text = Brower.SelectedPath;
            Controle.SetDestiny(Dir);
        }

        private void cmdBackup_Click(object sender, EventArgs e)
        {
            Controle.Backup();
        }
    }
}
