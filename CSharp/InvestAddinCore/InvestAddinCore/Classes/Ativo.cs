using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.Text.RegularExpressions;
using InvestAddinCore.Classes;

namespace InvestAddinCore.Classes
{
    class Ativo
    {
        public Ativo() 
        {
               
        }

        public string montaquery(RibbonMenu menucampos)
        {
            string campos = buscacampos(menucampos);
            
            string query = "SELECT [[CAMPO]] FROM TCLUCARTEIRA CAR"+
                           " [[JOIN]] "+
                           "WHERE CAR.DT_CARTEIRA = CONVERT(DateTime,'[[DATA]]',103) AND CLU.DS_FANTASIA LIKE '[[CLUBE]]'";
            if (string.IsNullOrEmpty(campos))
            {
                return("");
            }

            query = query.Replace("[[CAMPO]]", campos);
            query = query.Replace("[[JOIN]]", Join());

            if (campos.IndexOf("CAR.VL_PATRIMONIO") >= 0)
            {
                query += " ORDER BY CAR.VL_PATRIMONIO DESC";
            }
            else
            {
                if (campos.IndexOf("ATI.DS_ATIVO") >= 0)
                {
                    query += " ORDER BY ATI.DS_ATIVO";
                }
            }
            return(query);
        }

        static string Join()
        {
            string join = "LEFT JOIN TCLUATIVO ATI ON ATI.PK_ID = CAR.FK_ATIVO " +
                        "LEFT JOIN TCLUCLUBE CLU ON CLU.PK_ID = CAR.FK_CLUBE " +
                        "LEFT JOIN TCLUINSTITUICAO INS ON INS.PK_ID = ATI.FK_EMISSOR " +
                        "LEFT JOIN TCLUSETOR SETO ON SETO.PK_ID = ATI.FK_SETOR " +
                        "LEFT JOIN TCLUCLASSE CLA ON CLA.PK_ID = ATI.FK_CLASSE " +
                        "LEFT JOIN TCLUTIPOATIVO TIT ON TIT.PK_ID = ATI.FK_TIPOATIVO";

            return (join);
        }

        private string buscacampos(RibbonMenu menu)
        {
            string retorno = "";

            foreach (RibbonCheckBox check in menu.Items)
            {
                if (check.Checked)
                {
                    retorno += "," + check.Tag;
                }
            }
            if (string.IsNullOrEmpty(retorno))
            {
                return (retorno);
            }
            return (retorno.Substring(1));
        }
    }

}
