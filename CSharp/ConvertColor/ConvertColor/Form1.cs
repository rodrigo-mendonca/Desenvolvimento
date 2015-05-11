using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConvertColor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int nR = Convert.ToInt32(txtRed.Text);
            int nG = Convert.ToInt32(txtGreen.Text);
            int nB = Convert.ToInt32(txtBlue.Text);

            if (nR > 255 || nG > 255 || nB>255)
                return;

            RGBPanel.BackColor = Color.FromArgb(nR, nG, nB);

            byte[] bytes = new byte[4];
            bytes[0] = (byte)nR;
            bytes[1] = (byte)nG;
            bytes[2] = (byte)nB;
            int dValue = BitConverter.ToInt32(bytes, 0);
            txtDecimal.Text = dValue.ToString();
        }

        private void txtGreen_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRed_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtGreen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                )
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtBlue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                )
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                )
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int nDecimal = Convert.ToInt32(txtDecimal.Text);
            byte[] bytes = BitConverter.GetBytes(nDecimal);

            int nR = bytes[0];
            int nG = bytes[1];
            int nB = bytes[2];

            txtRed.Text = nR.ToString();
            txtGreen.Text = nG.ToString();
            txtBlue.Text = nB.ToString();

            RGBPanel.BackColor = Color.FromArgb(nR, nG, nB);
        }
    }
}
