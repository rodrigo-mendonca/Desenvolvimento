using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data;

namespace InvestAddinCore.Classes
{
    class BaseDados
    {
        public static void ExecutaSQL(string comando, DataTable datatable)
        {
            SqlConnection conexao = null;
            conexao = new SqlConnection(stringconnection());

            try
            {
                conexao.Open();
                SqlDataAdapter Command = new SqlDataAdapter(comando, conexao);
  
                Command.Fill(datatable);
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
                conexao.Dispose();
            }
            conexao.Dispose();
        }

        public static string stringconnection()
        {
            string conexao = "";

            return (conexao);
        }

        public static string extract(string text, string n1, string n2, int oco)
        {
            string retorno = "";

            int oco1 = 0;

            int oco2 = text.Length;

            if (!string.IsNullOrEmpty(n1))
            {
                oco1 = text.IndexOf(n1) + n1.Length;
            }

            if (!string.IsNullOrEmpty(n2))
            {
                oco2 = text.IndexOf(n2);
            }
           

            for (int i = 1; i < oco; i++)
            {
                oco1 = text.IndexOf(n1, oco1) + n1.Length;
                oco2 = text.IndexOf(n2, oco1);
            }

            if (oco1==0){return(retorno);}
            if (oco2==0){return(retorno);}

            retorno = text.Substring(oco1,oco2 -  oco1);

            return (retorno);
        }


        public static char Chr(int codigo)
	    {
	        return (char)codigo;
	    }

        public static int Asc(string letra)
	    {
	        return (int)(Convert.ToChar(letra));
	    }

    }

    class direito
    {
        public string clubes()
        {
            string retorno = "0";
            string usuario = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();

            string comando = @"
                SELECT 
                    PK_ID 
                FROM 
                    TCLUCLUBE 
                WHERE 
                PK_ID NOT IN(
                    SELECT 
	                    FK_CLUBE 
                    FROM 
	                    TCLUCLUBEUSUARIODIREITO 
                    WHERE 
	                    FK_USUARIO = (
		                    SELECT TOP 1 
			                    PK_ID 
		                    FROM 
			                    TCLUUSUARIO 
		                    WHERE 
			                    DS_LOGIN=('[login]') AND TG_RESTRITO1INV = 1		
                    ) AND NOT FK_USUARIO IS NULL
                ) AND TG_INATIVO = 0
                ORDER BY
                DS_FANTASIA ".Replace("[login]", BaseDados.extract(usuario, BaseDados.Chr(92).ToString(), "", 1));

            DataTable clubes = new DataTable();
            BaseDados.ExecutaSQL(comando, clubes);

            foreach (DataRow Row in clubes.Rows)
            {
                retorno += "," + Row["PK_ID"].ToString();
            }
            return (retorno.Substring(2));
        }    
    }
}