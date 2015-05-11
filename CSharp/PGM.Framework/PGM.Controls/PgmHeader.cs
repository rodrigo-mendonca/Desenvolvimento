using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGM.Controls
{
    public enum ControlType
    {
        TextBox,
        CheckBox,
        ComboBox
    }

    public class PgmHeader
    {
        public PgmHeader()
        { 
        
        }

         /// <summary>
        /// Nome do campo na lista
        /// </summary>
        public string FieldName = "";
        /// <summary>
        /// Nome que será exibido na grade
        /// </summary>
        public string ColumnName = "";
        /// <summary>
        /// Formato que o campo será exibido na grade
        /// </summary>
        public string Format = "";
        /// <summary>
        /// Quando nulo exibe o que estiver nessa propriedade
        /// </summary>
        public string NullValue = "";
        /// <summary>
        /// Tipo do controle usado para exibição do campo
        /// </summary>
        public ControlType Control = ControlType.TextBox;
    }
}