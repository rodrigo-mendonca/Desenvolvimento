using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PGM.Controls.PgmControl
{
    public partial class PgmDateTimePicker : DateTimePicker
    {
        #region Propriedades


        #endregion

        #region Construtores
        public PgmDateTimePicker()
        {
            InitializeComponent();
        }
        #endregion

        #region Metodos
            public DateTime GetValue()
            {
                return Convert.ToDateTime(Value);
            }

            public void SetValue(object tValue)
            {
                if (tValue != null)
                {
                    if (((DateTime)tValue).Year != 1)
                    {
                        Value = (DateTime)tValue;
                        Text = tValue.ToString();
                    }
                }
            }
        #endregion

        #region Eventos


        #endregion
    }
}
