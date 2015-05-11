using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGM.Excel
{
    public class PgmExcelHeader
    {
        public PgmExcelHeader()
        { 
        
        }
        /// <summary>
        /// Nome do campo na lista
        /// </summary>
        public string FieldName = "";
        /// <summary>
        /// Nome que será exibido no Excel
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
    }
}