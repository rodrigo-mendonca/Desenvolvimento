using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace SQL
{
    class InsertBuilder
    {
        public int nLastId;
        string cTable = "";
        string cQuery = "";
        List<Value> listFilds = new List<Value>();

        public InsertBuilder(string tcTable)
        {
            cTable = tcTable;
        }

        public void AddValues(Value toInsert)
        {
            listFilds.Add(toInsert);
        }

        void BuildQuery()
        {
            int nInd = 0;
            string cValues = "";
            foreach (Value oItem in listFilds)
            {
                if (cQuery != "") { cQuery += ","; }
                cQuery += oItem.GetName();
            }

            cQuery = "INSERT INTO " + cTable + " (" + cQuery + ") ";
            foreach (Value oItem in listFilds)
            {
                if (cValues != "") { cValues += ","; }
                cValues += "@" + nInd.ToString();
                nInd++;
            }
            cQuery += "VALUES(" + cValues + ")";
        }

        public void Insert()
        {
            BuildQuery();

            ConnectionBuilder oConnect = new ConnectionBuilder();
            SqlConnection oConexao = oConnect.Connect();

            try
            {
                this.cQuery += ";SELECT CAST(scope_identity() AS int)";

                SqlCommand oCom = new SqlCommand(this.cQuery, oConexao);
                SqlParameter oPar = new SqlParameter();
                int nI = 0;
                foreach (Value oI in listFilds)
                {
                    oPar = new SqlParameter("@" + nI.ToString(), oI.GetValue());
                    oCom.Parameters.Add(oPar);
                    nI++;
                }

                oConexao.Open();
                nLastId = (Int32) oCom.ExecuteScalar();
            }
            catch (Exception erro)
            {
                oConnect.DisposeConnect();
            }
            oConnect.DisposeConnect();
            this.cQuery = "";
        }
    }

    class Value
    {
        Object oValue;
        string cName;
        string cCond;

        public Value(string tcName)
        {
            cName = tcName;
        }

        public Value SetValue(Object toValue)
        {
            oValue = toValue;
            return(this);
        }

        public Value SetCond(string tcValue)
        {
            cCond = tcValue;
            return this;
        }
        public string GetName()
        {
            return (cName);
        }
        public string GetCond()
        {
            return (cCond);
        }
        public Object GetValue()
        {
            return (oValue);
        }
    }

    class ValueIN
    {
        List<Value> oValue = new List<Value>();
        string cName;

        public ValueIN(string tcName)
        {
            cName = tcName;
        }

        public ValueIN SetValue(Value toValue)
        {
            oValue.Add(toValue);
            return (this);
        }

        public string GetName()
        {
            return (cName);
        }

        public List<Value> GetValueList()
        {
            return (oValue);
        }
    }
}