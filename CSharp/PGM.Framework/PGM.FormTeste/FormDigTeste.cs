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
using PGM.SQL.Models;
using PGM.Interfaces;

namespace PGM.FormTeste
{
    public partial class FormDigTeste : FormDig
    {
        public FormDigTeste() // Passa para a herença qual é a classe que o form vai trabalhar
        {
            InitializeComponent();            
        }
        private void FormDigTeste_PgmRegister(object sender, EventArgs e)
        {
            // aqui define que model utilizar no digitar
            // o register fala que campo pertence a que propriedade
            SetModel<Teste>()
                .Register(i => i.PkId, txtID)
                .Register(i => i.Desc, txtDesc)
                .Register(i => i.Data, txtData)
                .Register(i => i.Valor, txtValor);

            //SetConnection("SysConnection");
        }
    }
}
