using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PGMProcessing
{
    public partial class Histogram : Form
    {
        public int[] Colors;
        public int[] Accum;

        public Histogram()
        {
            InitializeComponent();            
        }

        private void Histogram_Load(object sender, EventArgs e)
        {
            cboTIPO.Text = "Cores";
            LoadChart(Colors,0);
        }

        public void LoadChart(int[] tList,int tMax)
        {
            Chart.Series.Clear();
            Series oSer = new Series("Quantidade");
            oSer.Color = Color.Red;
            oSer.IsVisibleInLegend = false;
            for (int i = 0; i < tList.Length; i++)
            {
                oSer.Points.Add(tList[i]);
            }
            Chart.Series.Add(oSer);
            Chart.ChartAreas[0].AxisX.Interval = 10;
            if (tMax > 0)
                Chart.ChartAreas[0].AxisY.Maximum = tMax;
        }

        private void cboTIPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTIPO.Text == "Cores")
                LoadChart(Colors,0);
            else
                LoadChart(Accum, Accum[Accum.Length - 1]);
        }
    }
}
