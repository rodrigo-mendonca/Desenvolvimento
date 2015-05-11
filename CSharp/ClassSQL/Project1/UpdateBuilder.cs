using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace SQL
{
    class UpdateBuilder
    {
        string cTable = "";
        string cQuery = "";
        List<Value> listFilds = new List<Value>();
        List<Value> listWhere = new List<Value>();
        List<ValueIN> listWhereIn = new List<ValueIN>();

        public UpdateBuilder(string tcTable)
        {
            cTable = tcTable;
        }

        void BuildQuery()
        {
            int nInd = 0;
            string cWhere = "", cFields = "", cIn = "";
            foreach (Value oItem in listFilds)
            {
                if (cFields != "") { cFields += ","; }

                cFields += oItem.GetName() + "=@"+nInd.ToString();
                nInd++;
            }

            foreach (Value oItem in listWhere)
            {
                if (cWhere != "") { cWhere += " AND "; }
                cWhere += oItem.GetName() + oItem.GetCond() + "@" + nInd.ToString();
                nInd++;
            }

            foreach (ValueIN oIn in listWhereIn)
            {
                if (cWhere != "") { cWhere += " AND "; }

                cWhere += oIn.GetName() + "IN(";
                cIn = "";
                foreach (Value oItem in oIn.GetValueList())
                {
                    cIn += ",@" + nInd.ToString();
                    nInd++;
                }
                cWhere = cIn.Substring(1) + ")";
            }

            cQuery = "UPDATE " + cTable + " SET " + cFields;
            if (cWhere != "")
                cQuery += " WHERE " + cWhere;
        }

        public void AddValues(Value toValue)
        {
            listFilds.Add(toValue);
        }

        public void AddWhere(Value toValue)
        {
            listWhere.Add(toValue);
        }

        public void AddWhereIn(ValueIN toValue)
        {
            listWhereIn.Add(toValue);
        }

        public void Update()
        {
            BuildQuery();

            ConnectionBuilder oConnect = new ConnectionBuilder();
            SqlConnection oConexao = oConnect.Connect();

            try
            {
                SqlCommand oCom = new SqlCommand(this.cQuery, oConexao);
                SqlParameter oPar;
                int nI = 0;
                foreach (Value oI in listFilds)
                {
                    oPar = new SqlParameter("@" + nI.ToString(), oI.GetValue());
                    oCom.Parameters.Add(oPar);
                    nI++;
                }
                foreach (Value oI in listWhere)
                {
                    oPar = new SqlParameter("@" + nI.ToString(), oI.GetValue());

                    oCom.Parameters.Add(oPar);
                    nI++;
                }

                foreach (ValueIN oIn in listWhereIn)
                {
                    foreach (Value oItem in oIn.GetValueList())
                    {
                        oPar = new SqlParameter("@" + nI.ToString(), oItem.GetValue());

                        oCom.Parameters.Add(oPar);
                        nI++;
                    }
                }

                oConexao.Open();
                oCom.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                oConnect.DisposeConnect();
            }
            oConnect.DisposeConnect();
            this.cQuery = "";
        }
    }
}
