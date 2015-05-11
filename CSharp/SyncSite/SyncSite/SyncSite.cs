using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SyncSite
{
    public partial class SyncSite : Form
    {
        public SyncSite()
        {
            InitializeComponent();
        }

        private void cmdIniciar_Click(object sender, EventArgs e)
        {
            Time.Enabled    = !Time.Enabled;
            txtHora.Enabled = !Time.Enabled;

            if (Time.Enabled)
                cmdIniciar.Text = "Parar";
            else
                cmdIniciar.Text = "Iniciar";
            
        }

        private void Time_Tick(object sender, EventArgs e)
        {
            MessageBox.Show("!");
        }
    }
}
