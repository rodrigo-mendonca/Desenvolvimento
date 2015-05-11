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
    public partial class PgmTextBox : MaskedTextBox, IControl
    {

        #region Propriedades
        public enum Type
        {
            String,
            Integer,
            Decimal,
            Date
        }

        /// <summary>
        /// Propriedade indica o tipo de dados que a textbox vai trabalhar
        /// </summary>
        [Description("Propriedade indica o tipo de dados que a textbox vai trabalhar")]
        public Type DataType{ get; set; }

        /// <summary>
        /// Propriedade indica para tipos number que é para deixar o valor Vazio se for zero na textbox
        /// </summary>
        [Description("Propriedade indica para tipos number que é para deixar o valor Vazio se for zero na textbox")]
        public bool BlankIfZero { get; set; }

        private bool _Obrigatory;
        /// <summary>
        /// Propriedade indica se o campo é obrigatorio
        /// </summary>
        [Description("Propriedade indica se o campo é obrigatorio")]
        public bool Obrigatory { get { return this._Obrigatory; } set { this._Obrigatory = SetObrigatory(value); } }

        #endregion

        #region Construtores
        public PgmTextBox()
        {
            InitializeComponent();

            //Padrão
            DataType = Type.String;
            this.KeyPress += new KeyPressEventHandler(PgmTextBox_KeyPress);
            this.Enter += new EventHandler(PgmTextBox_Enter);
            this.Leave += new EventHandler(PgmTextBox_Leave);
        }
        #endregion

        #region Metodos

        /// <summary>
        /// Inclui o valor na text, verificando se o tipo é compativel com o tipo definido na propriedade DataType.
        /// </summary>
        /// <param name="tText">Valor para a Text</param>
        public void SetValue(object tText) 
        {
            if (tText == null)
            {
                Text = "";
                return;
            }
            switch (DataType)
            {
                case Type.String:
                    if (tText != null)
                        Text = tText.ToString();
                    else
                        Text = "";
                    break;
                case Type.Integer:
                    if (tText.GetType() == typeof(int))
                    {
                        Text = tText.ToString();
                        if (Convert.ToInt16(tText) == 0 && BlankIfZero)
                            Text = "";
                    }
                    else
                        Text = "";
                    break;
                case Type.Decimal:
                    if (tText.GetType() == typeof(decimal))
                    {
                        Text = tText.ToString();
                        if (Convert.ToDecimal(tText) == 0 && BlankIfZero)
                            Text = "";
                    }
                    else
                        Text = "";
                    break;
                case Type.Date:
                    if (tText.GetType() == typeof(DateTime))
                    {
                        Text = (Convert.ToDateTime(tText)).ToString("d");
                        if (Text == "01/01/0001")
                            Text = "";
                    }
                    else
                        Text = "";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Pegar o valor da text, convertendo de acordo com o tipo.
        /// </summary>
        /// <returns>Retorna o valor que está na Text como object</returns>
        public object GetValue()
        {
            object oReturn;

            switch (DataType)
            {
                case Type.Decimal:
                    if (Text != "")
                        oReturn = Convert.ToDecimal(Text);
                    else
                        oReturn = 0;
                    break;
                case Type.Integer:
                    if (Text != "")
                        oReturn = Convert.ToInt16(Text);
                    else
                        oReturn = 0;
                    break;
                case Type.Date:
                    if (Text != "")
                        oReturn = Convert.ToDateTime(Text);
                    else
                        oReturn = null;
                    break;
                default:
                    oReturn = Text;
                    break;
            }

            return oReturn;
        }

        /// <summary>
        /// Altera a cor da text para a cor padrão de obrigatorio
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
            bool Return = false;
            switch (DataType)
            {
                case Type.Decimal:
                    if (Convert.ToDecimal(Text) == 0)
                        Return = true;
                    break;
                case Type.Integer:
                    if (Convert.ToInt16(Text) == 0)
                        Return = true;
                    break;
                default:
                    if (Text == "")
                        Return = true;
                    break;
            }

            return (Return);
        }

        #endregion

        #region Eventos
        private void PgmTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (DataType == Type.Integer || DataType ==  Type.Decimal)
            {
                e.Handled = !((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar=='-') && !char.IsControl(e.KeyChar);
            }
        }
        private void PgmTextBox_Enter(object sender, EventArgs e)
        {
            if (DataType == Type.Date)
            {
                this.Mask = "00/00/0000";
            }
        }
        private void PgmTextBox_Leave(object sender, EventArgs e)
        {
            if (DataType == Type.Date)
            {
                DateTime temp;
                if (!DateTime.TryParse(this.Text, out temp))
                {
                    this.Text = "";
                    this.Mask = " ";
                }
            }
        }
        #endregion
    }
}
