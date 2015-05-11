using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace SQL
{
    class ConnectionBuilder
    {
        SqlConnection oConexao = null;
        string cConexao;
        string cFileConnect = @"X:\INVEST\InvestExcel\Conexao.cfg";

        public ConnectionBuilder()
        {
        }

        public ConnectionBuilder(string tFileConnect)
        {
            cFileConnect = tFileConnect;
        
        }

        public SqlConnection GetConnection()
        {
            return oConexao;
        }

        public SqlConnection Connect()
        {
            TextReader oText = new StreamReader(cFileConnect);
            cConexao = oText.ReadLine();
            oText.Close();
            return ReConnect();
        }

        public SqlConnection ReConnect()
        {
            oConexao = new SqlConnection(cConexao);
            try
            {
                oConexao.Open();
                oConexao.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return oConexao;
        }

        public void DisposeConnect()
        {
            TextReader oText = new StreamReader(cFileConnect);
            cConexao = oText.ReadLine();
            oText.Close();
            ReConnect();
        }
    }
}
