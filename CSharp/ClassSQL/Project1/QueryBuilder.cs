using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace SQL
{
    class CondValue
    {
        public object dValue;
        public int nIndex;

        public CondValue(int tIndex, object tValue)
        {
            dValue = tValue;
            nIndex = tIndex;
        }
    }

    public class QueryBuilder
    {
        string cCampos              = "";
        string cTabela              = "";
        string cAs                  = "";
        List<String> listCond       = new List<String>();
        List<CondValue> listValues  = new List<CondValue>();
        List<String> listJoin       = new List<String>();
        int nTotCond                = 0;
        List<String> listOrder = new List<String>();
        List<String> listGroup      = new List<String>();
        string cTop                 = "";
        List<List<object>> dResult  = new List<List<object>>();
        DataTable oTable;
            
        public QueryBuilder()
        { 
        }

        public object Resultado()
        {
            object dRetorno = dResult[0][0];
            return (dRetorno);
        }

        public int Count()
        {
            return (dResult.Count);
        }

        public DataTable ResultadoDataTable()
        {
            return (oTable);
        }

        public void ConfigGrid(DataGrid oGrid)
        {
            oGrid.DataSource = oTable;
        }

        public List<object> ResultadoLinha()
        {
            return (dResult[0]);
        }

        public List<List<object>> ResultadoMatriz()
        {
            return (dResult);
        }

        public QueryBuilder AddTop(int tnTop)
        {
            cTop =  tnTop.ToString();
            return (this);
        }

        public QueryBuilder AddCampo(string tcCampo)
        {
            cCampos += "," + tcCampo.Trim();
            return (this);
        }

        public QueryBuilder AddCampo(string tcCampo,string tcAs)
        {
            cCampos += "," + tcCampo.Trim() + " AS " + tcAs.Trim();
            return (this);
        }

        public QueryBuilder AddTabela(string tcTabela)
        {
            cTabela = tcTabela;
            return (this);
        }

        public QueryBuilder AddTabela(string tcTabela, string tcAs)
        {
            cTabela = tcTabela;
            cAs     = tcAs;
            return (this);
        }

        public QueryBuilder AddCondicao(string cOpe,string tcCampo, string tcCond, object dCampo)
        {
            string cCond = "";

            cCond += " "+ cOpe.Trim() + " " + tcCampo.Trim() + " " + tcCond.Trim() + " @" + nTotCond.ToString();

            listCond.Add(cCond);
            CondValue oCampo = new CondValue(nTotCond, dCampo);
            listValues.Add(oCampo);
            nTotCond++;
            return (this);
        }

        public QueryBuilder AddCondicao(int lChave, string cOpe, string tcCampo, string tcCond, object dCampo)
        {
            string cCond = "";

            cCond += " " + cOpe.Trim() + " ";
            if (lChave == 1)
                cCond += " (";

            cCond += tcCampo.Trim() + " " + tcCond.Trim() + " @" + nTotCond.ToString();

            if (lChave == 2)
                cCond += ") ";

            listCond.Add(cCond);
            CondValue oCampo = new CondValue(nTotCond, dCampo);
            listValues.Add(oCampo);
            nTotCond++;
            return (this);
        }

        public QueryBuilder AddCondicaoIn(string tcCampo, object[] dCampo)
        {
            string cCond = "";
            cCond += tcCampo.Trim() + " IN(";

            foreach (object dItem in dCampo)
            {
                CondValue oCampo = new CondValue(nTotCond, dItem);

                cCond += "@" + nTotCond.ToString() + ",";

                listValues.Add(oCampo);
                nTotCond++;
            }
            cCond = cCond.Substring(0,cCond.Length-1) + ")";
            listCond.Add(cCond);
            return (this);
        }

        public QueryBuilder AddCondicaoLike(string tcCampo, string cAt1, string dCampo, string cAt2)
        {
            string cCond = "", cCampo = "";

            cCampo = tcCampo;

            cCond += tcCampo.Trim() + " LIKE " + "@" + nTotCond.ToString() + "";
            listCond.Add(cCond);
            CondValue oCampo = new CondValue(nTotCond, cAt1 + dCampo + cAt2);
            listValues.Add(oCampo);
            nTotCond++;
            return (this);
        }

        public QueryBuilder AddJoin(string tcTabela,string tcAs,string tcCond)
        {
            string cJoin = "LEFT JOIN " + tcTabela.Trim() + " " + tcAs.Trim() + " ON " + tcCond.Trim();
            listJoin.Add(cJoin);
            return (this);
        }

        public QueryBuilder AddOrder()
        {
            listOrder.Add(cCampos.Substring(2));
            return (this);
        }

        public QueryBuilder AddOrder(string tcOrder)
        {
            listOrder.Add(tcOrder);
            return (this);
        }

        public QueryBuilder AddGroup()
        {
            listGroup.Add(cCampos.Substring(2));
            return (this);
        }

        public QueryBuilder AddGroup(string tcGroup)
        {
            listGroup.Add(tcGroup);
            return (this);
        }

        public string Query()
        {
            string cQuery = "SELECT ";

            if (cTop != "")
                cQuery += " TOP " + cTop + " ";

            cQuery += cCampos.Substring(1);
            cQuery += " FROM ";
            cQuery += cTabela + " " + cAs + " ";

            foreach (String oI in listJoin)
            {
                cQuery += oI;
            }
            if (listCond.Count > 0)
                cQuery += " WHERE ";
            int nI = 0;
            foreach (String oI in listCond)
            {
                if (nI == 0)
                {
                    cQuery += oI.Replace("AND","").Replace("OR","");
                    nI = 1;
                }
                else
                {
                    cQuery += oI;
                }
            }

            if (listGroup.Count > 0)
            {
                nI = 0;
                cQuery += " GROUP BY ";
                foreach (String oI in listGroup)
                {
                    if (nI == 0)
                    {
                        cQuery += oI.Trim();
                        nI = 1;
                    }
                    else
                        cQuery += "," + oI.Trim();
                }
            }

            if (listOrder.Count > 0)
            {
                nI = 0;
                cQuery += " ORDER BY ";
                foreach (String oI in listOrder)
                {
                    if (nI == 0){
                        cQuery += oI.Trim();
                        nI = 1;
                    }
                    else
                        cQuery += "," + oI.Trim();
                }
            }
            return cQuery;
        }

        public void Executar()
        {
            string cQuery = Query();
            ExecutaSQL(cQuery);
        }

        public void ExecutaSQL(string tcComando)
        {
            ConnectionBuilder oConnect = new ConnectionBuilder();
            SqlConnection oConexao = oConnect.Connect();

            try
            {
                SqlCommand oCom = new SqlCommand(tcComando, oConexao);
                SqlParameter oPar = new SqlParameter();

                foreach (CondValue oI in listValues)
                {
                    oCom.Parameters.AddWithValue("@" + oI.nIndex, oI.dValue);
                }

                oConexao.Open();
                SqlDataAdapter oCommand = new SqlDataAdapter(oCom);

                oCommand.Fill(oTable);
                

                DataRow oRow;
                List<dynamic> dRow;
                dResult.Clear();

                for (int i = 0; i < oTable.Rows.Count; i++)
			    {
                    oRow = oTable.Rows[i];
                    dRow = new List<dynamic>();
			        for (int j = 0; j < oRow.Table.Columns.Count; j++)
			        {
			             dRow.Add(oRow[j]);
			        }
                    dResult.Add(dRow);
			    }
            }
            catch (Exception erro)
            {

                oConnect.DisposeConnect();
            }
            oConnect.DisposeConnect();
        }
    }
}