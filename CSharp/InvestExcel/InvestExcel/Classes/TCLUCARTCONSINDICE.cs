using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQL;

namespace InvestExcel.Classes
{
    class TCLUCARTCONSINDICE
    {
        public int PK_ID;
        public string DS_CLUBES;
        public int TG_NIVEL;
        public int TG_CONSIDERAR;
        public int TG_LCIBRUTA;
        public DateTime DT_MINIMA;
        public DateTime DT_MAXIMA;
        public DateTime DT_ATUAL;
        public DateTime DT_PROCESSAMENTO;
        public int TG_PUBLICADO;
        public int FK_CLUBEPROCESSADO;
        public int TG_INATIVO;
        public int FK_OWNER;
        public DateTime DH_INCLUSAO;
        public DateTime DH_ALTERACAO;
        public DateTime DT_PUBLICADODE;
        public DateTime DT_PUBLICADOATE;

        public TCLUCARTCONSINDICE(object[,] Fundo, int Nivel, int Lci, int Desconsiderar)
        {
            if (!SysFuncoes.VerifDireitoClube(Fundo))
                return;

            string cListInvestimento = SysFuncoes.MontarIn(Fundo);
            
            QueryBuilder oQuery = new QueryBuilder();

            oQuery.AddCampo("IND.*");
            oQuery.AddTabela("TCLUCARTCONSINDICE", "IND");
            oQuery.AddCondicaoLike("IND.DS_CLUBES", "", cListInvestimento, "");
            oQuery.AddCondicao("AND", "IND.TG_NIVEL", "=", Nivel);
            oQuery.AddCondicao("AND", "IND.TG_CONSIDERAR", "=", Desconsiderar);
            oQuery.AddCondicao("AND", "IND.TG_LCIBRUTA", "=", Lci);
            oQuery.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oQuery.ResultadoMatriz());

            this.PK_ID              = Convert.ToInt32(oRetorno[0,0]);
            this.DS_CLUBES          = oRetorno[0,1].ToString();
            this.TG_NIVEL           = Convert.ToInt32(oRetorno[0,2]);
            this.TG_CONSIDERAR      = Convert.ToInt32(oRetorno[0,3]);
            this.TG_LCIBRUTA        = Convert.ToInt32(oRetorno[0,4]);

            if (typeof(DBNull) != oRetorno[0, 5].GetType())
                this.DT_MINIMA          = (DateTime)oRetorno[0, 5];
            if (typeof(DBNull) != oRetorno[0, 6].GetType())
                this.DT_MAXIMA = (DateTime)oRetorno[0, 6];
            if (typeof(DBNull) != oRetorno[0, 7].GetType())
                this.DT_ATUAL = (DateTime)oRetorno[0, 7];
            if (typeof(DBNull) != oRetorno[0, 8].GetType())
                this.DT_PROCESSAMENTO = (DateTime)oRetorno[0, 8];

            this.TG_PUBLICADO       = Convert.ToInt32(oRetorno[0,9]);
            this.FK_CLUBEPROCESSADO = Convert.ToInt32(oRetorno[0,10]);
            this.TG_INATIVO         = Convert.ToInt32(oRetorno[0,11]);
            this.FK_OWNER           = Convert.ToInt32(oRetorno[0, 12]);

            if (typeof(DBNull) != oRetorno[0,13].GetType())
                this.DH_INCLUSAO = (DateTime)oRetorno[0,13];
            if (typeof(DBNull) != oRetorno[0,14].GetType())
                this.DH_ALTERACAO = (DateTime)oRetorno[0,14];
            if (typeof(DBNull) != oRetorno[0,15].GetType())
                this.DT_PUBLICADODE = (DateTime)oRetorno[0,15];
            if (typeof(DBNull) != oRetorno[0,16].GetType())
                this.DT_PUBLICADOATE = (DateTime)oRetorno[0,16];
        }
    }
}
