using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace Invest
{
    [ProgId("Invest.SQL")]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class SQL
    {
        Excel.Application oExcel = null;
        string cCampos            = "";
        string cComando           = "";
        string cConexao           = "";
        string cTabela            = "";
        string cAs                = "";
        List<String> listCond     = new List<String>();
        List<Object> listValues   = new List<Object>();
        List<String> listJoin     = new List<String>();
        string cOrder             = "";
        string cGroup             = "";
        DataTable dResult         = null;

        public SQL()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toExcel"></param>
        public void Excel(Excel.Application toExcel)
        {
            oExcel = toExcel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Query"></param>
        public void AddComando(string Comando)
        {
            cComando = Comando;
        }

        /// <summary>
        /// Campo para adicionar na pesquisa
        /// </summary>
        /// <param name="Campo">Nome do campo na base de dados</param>
        /// <param name="Apelido">Apelido para o campo(Na duvida, adicionar as 3 primeiras letras depois do underline)</param>
        /// <returns></returns>
        public void AddCampo(string Campo, string Apelido = "")
        {
            cCampos += "," + Campo.Trim();
            if (Apelido!="")
	        {
		        cCampos += " AS " + Apelido.Trim();
	        }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Tabela"></param>
        /// <param name="Apelido"></param>
        /// <returns></returns>
        public void AddTabela(string Tabela, string Apelido = "")
        {
            cTabela = Tabela;
            cAs     = Apelido;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Campo"></param>
        /// <param name="Condicao"></param>
        /// <param name="Valor"></param>
        /// <returns></returns>
        public void AddCondicao(string Campo, string Condicao, Object Valor)
        {
            string cCond = "";
            
            cCond += Campo.Trim() + " " + Condicao.Trim() + " @" + listCond.Count.ToString();
            listCond.Add(cCond);
            listValues.Add(Valor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Tabela"></param>
        /// <param name="Apelido"></param>
        /// <param name="Condicao"></param>
        /// <returns></returns>
        public void AddJoin(string Tabela, string Apelido, string Condicao)
        {
            string cJoin = " LEFT JOIN " + Tabela.Trim() + " " + Apelido.Trim() + " ON " + Condicao.Trim();
            listJoin.Add(cJoin);            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Campo"></param>
        /// <returns></returns>
        public void AddOrdem(string Campo)
        {
            cOrder = Campo;           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Campo"></param>
        /// <returns></returns>
        public void AddAgrup(string Campo)
        {
            cGroup = Campo;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Executar()
        {
            if (cComando!="")
            {
                ExecutaSQL(cComando);
                return cComando;
            }

            string cQuery = "SELECT ";

            cQuery+= cCampos.Substring(1);
            cQuery += " FROM ";
            cQuery += cTabela + " " + cAs + " ";

            foreach (String oI in listJoin)
            {
                cQuery += oI;
            }

            if (listCond.Count!=0)
            {
                cQuery += " WHERE ";
            }
            int nI = 0;
            foreach (String oI in listCond)
            {
                if (nI == 0)
                {
                    cQuery += oI;
                    nI = 1;
                }
                else
                {
                    cQuery += " AND " + oI;
                }
            }

            if (cGroup != "")
            {
                cQuery += " GROUP BY " + cGroup;
            }
            if (cOrder != "")
            {
                cQuery += " ORDER BY " + cOrder;
            }
            ExecutaSQL(cQuery);
            return (cQuery);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Celula"></param>
        public void PreencherExcel(string Celula)
        {
            int nRow = oExcel.Range[Celula].Row;
            int nCol = 0;

            nCol = oExcel.Range[Celula].Column;
            for (int i = 0; i < dResult.Columns.Count; i++)
            {
                oExcel.Cells[nRow, nCol] = dResult.Columns[i].ColumnName;
                nCol++;
            }

            nRow++;
            for (int nI = 0; nI < dResult.Rows.Count; nI++)
            {
                nCol = oExcel.Range[Celula].Column;
                for (int nJ = 0; nJ < dResult.Columns.Count; nJ++)
                {
                    oExcel.Cells[nRow, nCol] = dResult.Rows[nI][nJ];
                    nCol++;
                }
                nRow++;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Object[,] GeraVetorResult()
        {
            Object[,] oRetorno = new Object[dResult.Rows.Count,dResult.Columns.Count];

            for (int nI = 0; nI < dResult.Rows.Count; nI++)
            {
                for (int nJ = 0; nJ < dResult.Columns.Count; nJ++)
                {
                    oRetorno[nI, nJ] = dResult.Rows[nI][nJ];
                }
            }

            return (oRetorno);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tcComando"></param>
        private void ExecutaSQL(string tcComando)
        {
            SqlConnection oConexao = null;
            oConexao = new SqlConnection(cConexao);
            try
            {
                oConexao.Open();
                oConexao.Close();
            }
            catch (Exception)
            {
                oConexao = new SqlConnection(cConexao);
                throw;
            }

            DataTable oTable = new DataTable();
            try
            {
                SqlCommand oCom = new SqlCommand(tcComando, oConexao);
                SqlParameter oPar = new SqlParameter();
                int nI = 0;
                foreach (var oI in listValues)
                {
                    oPar = new SqlParameter("@" + nI.ToString(), oI);
                    oCom.Parameters.Add(oPar);
                    nI++;
                }
                
                oConexao.Open();
                SqlDataAdapter oCommand = new SqlDataAdapter(oCom);

                oCommand.Fill(oTable);
                dResult = oTable;
            }
            catch (Exception erro)
            {
                oConexao.Dispose();
            }
            oConexao.Dispose();
        }
    }
}