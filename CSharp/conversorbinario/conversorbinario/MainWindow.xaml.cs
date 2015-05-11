using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using conversorbinario.Classes;

namespace conversorbinario
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void cmdDECIMAL_Click(object sender, RoutedEventArgs e)
        {
            txtRESULT.Text = Binario.toDECIMAL(txtTEXT.Text).ToString();
            txtLOG.Text = Binario.calclog();
        }

        private void cmdBINARIO_Click(object sender, RoutedEventArgs e)
        {
            txtRESULT.Text = Binario.toBinario(Convert.ToDouble(txtTEXT.Text)).Value();
            txtLOG.Text = Binario.calclog();
        }

        private void cmdDESTRING_Click(object sender, RoutedEventArgs e)
        {
            txtRESULT.Text = Binario.toBinario(txtTEXT.Text);
        }

        private void cmdPARASTRING_Click(object sender, RoutedEventArgs e)
        {
            txtRESULT.Text = Binario.toSTRING(txtTEXT.Text);
        }

        private void cmdDECIMALTOHEX_Click(object sender, RoutedEventArgs e)
        {
            txtRESULT.Text = Hex.toHex(Convert.ToDouble(txtTEXT.Text));
        }
    }
}
