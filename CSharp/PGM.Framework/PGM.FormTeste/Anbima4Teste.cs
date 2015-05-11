using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PGM.Controls;
using PGM.Xml;
using PGM.Xml.Models.Anbima4;
using System.Reflection;

namespace PGM.FormTeste
{
    public partial class Anbima4Teste : FormBase
    {
        Anbima4 oAn;

        public Anbima4Teste()
        {
            InitializeComponent();
        }

        private void Anbima4Teste_Load(object sender, EventArgs e)
        {
            ListTopicos.Items.Add("Header");
            ListTopicos.Items.Add("Caixa");
            ListTopicos.Items.Add("Cotas");
            ListTopicos.Items.Add("Despesas");
            ListTopicos.Items.Add("OutrasDespesas");
            ListTopicos.Items.Add("Provisao");
        }

        private void pgmButton1_Click(object sender, EventArgs e)
        {
            PgmXml oXml = new PgmXml();

            OpenFileDialog oOpen = new OpenFileDialog();
            oOpen.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            oOpen.ShowDialog();

            if (oOpen.FileName != "")
            {
                oXml.SetXmlFile(oOpen.FileName);
                oAn = oXml.Deserialize<Anbima4>();

                //Grid.SetDataSource(oAn.Fundo.Header);
            }
        }

        private void ListTopicos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (oAn != null)
            {
                int Index = ListTopicos.SelectedIndex;
                string Topico = ListTopicos.Items[Index].ToString();

                var List = oAn.Fundo.GetType().GetProperty(Topico).GetValue(oAn.Fundo);
                Grid.SetDataSource(List);
            }
        }
    }
}
