using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PGM.Extensions.Pgm;
using PGM.Interfaces;

namespace PGM.Controls.PgmControl
{
    public partial class PgmEditBox : TextBox,IControl
    {
        private bool _Obrigatory;
        /// <summary>
        /// Propriedade indica se o campo é obrigatorio
        /// </summary>
        [Description("Propriedade indica se o campo é obrigatorio")]
        public bool Obrigatory { get { return this._Obrigatory; } set { this._Obrigatory = SetObrigatory(value); } }


        public PgmEditBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Inclui o valor na editbox
        /// </summary>
        /// <param name="tText">Valor para a Text</param>
        public void SetValue(object tText)
        {
            if (tText != null)
                Text = tText.ToString();
        }

        /// <summary>
        /// Altera a cor da editbox para a cor padrão de obrigatorio
        /// </summary>
        /// <returns>Retorna o valor que recebeu</returns>
        public bool SetObrigatory(bool lOb)
        {
            Color oColor = new Color();
            if (lOb)
                this.BackColor = oColor.Obrigatory();
            else
                this.BackColor = Color.White;

            return lOb;
        }
        /// <summary>
        /// Pegar o valor da editbox
        /// </summary>
        /// <returns>Retorna o valor que está na editbox</returns>
        public object GetValue()
        {
            return Text;
        }

        /// <summary>
        /// Verifica se o campo é obrigatorio
        /// </summary>
        /// <returns>true ou false para indicar se é obrigatorio</returns>
        public bool IsObrigatory()
        {
            return _Obrigatory;
        }

        /// <summary>
        /// Verifica se o campo está vazio
        /// </summary>
        /// <returns>true ou false para indicar se está vazio</returns>
        public bool IsEmpty()
        {
            return Text == "";
        }
    }
}
