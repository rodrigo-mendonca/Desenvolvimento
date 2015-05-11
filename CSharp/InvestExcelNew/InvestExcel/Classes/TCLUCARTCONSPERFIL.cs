using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQL;

namespace InvestExcel.Classes
{
    public class TCLUCARTCONSPERFIL
    {
        public int PK_ID;
        public string DS_CLUBES;
        public int TG_NIVEL;
        public int TG_DESCONSIDERAR;
        public int TG_LCIBRUTA;
        public int TG_INATIVO;
        public int FK_OWNER;
        public DateTime DH_INCLUSAO;
        public DateTime DH_ALTERACAO;
        public int TG_ESPECIAL;
        public int NR_DECIMAIS;
        public int FK_APPADRAO;

        public TCLUCARTCONSPERFIL(object[,] Fundo)
        {
            if (!SysFuncoes.VerifDireitoClube(Fundo))
                return;

            QueryBuilder oQuery = new QueryBuilder();

            // SE FOR APENAS UM INVESTIMENTO, BUSCA DO PERFIL
            if (Fundo.GetLength(0) == 1 && Fundo.GetLength(1) == 1)
            {
                int nFundo = Convert.ToInt32(Fundo[0,0]);
                oQuery.AddCampo("PER.*");
                oQuery.AddCondicao("AND", "PER.DS_CLUBES", "=", nFundo.ToString("0000"));
            }
            else
            {
                string[] cListInvestimento = SysFuncoes.MatrizToVetorString(Fundo,"0000");

                oQuery.AddCampo("CAST(0 AS INTEGER)"    , "PK_ID");
                oQuery.AddCampo("CAST('' AS CHAR(20))"  , "DS_CLUBES");
                oQuery.AddCampo("MAX(TG_NIVEL)"         , "TG_NIVEL");
                oQuery.AddCampo("MAX(TG_DESCONSIDERAR)" , "TG_DESCONSIDERAR");
                oQuery.AddCampo("MAX(TG_LCIBRUTA)"      , "TG_LCIBRUTA");
                oQuery.AddCampo("CAST(0 AS INTEGER)"    , "TG_INATIVO");
                oQuery.AddCampo("CAST(0 AS INTEGER)"    , "FK_OWNER");
                oQuery.AddCampo("NULL"                  , "DH_INCLUSAO");
                oQuery.AddCampo("NULL"                  , "DH_ALTERACAO");
                oQuery.AddCampo("CAST(0 AS INTEGER)"    , "TG_ESPECIAL");
                oQuery.AddCampo("MAX(NR_DECIMAIS)"      , "NR_DECIMAIS");
                oQuery.AddCampo("CAST(0 AS INTEGER)"    , "FK_APPADRAO");
                oQuery.AddCondicaoIn("PER.DS_CLUBES"    ,  cListInvestimento);
            }

            oQuery.AddTabela("TCLUCARTCONSPERFIL", "PER");
            oQuery.Executar();

            // se não existir perfil, forca o nivel um para evitar erro
            if (oQuery.Count() == 0)
            {
                this.TG_NIVEL         = 1;
                this.TG_DESCONSIDERAR = 1;
                this.TG_LCIBRUTA      = 1;
                return;
            }

            object[,] oRetorno = ExcelDna.ListToMatriz(oQuery.ResultadoMatriz());

            this.PK_ID = Convert.ToInt32(oRetorno[0, 0]);
            this.DS_CLUBES = oRetorno[0, 1].ToString();
            this.TG_NIVEL = Convert.ToInt32(oRetorno[0, 2]);
            this.TG_DESCONSIDERAR = Convert.ToInt32(oRetorno[0, 3]);
            this.TG_LCIBRUTA = Convert.ToInt32(oRetorno[0, 4]);
            this.TG_INATIVO = Convert.ToInt32(oRetorno[0, 5]);
            this.FK_OWNER = Convert.ToInt32(oRetorno[0, 6]);

            if(typeof(DBNull) != oRetorno[0, 7].GetType())
                this.DH_INCLUSAO = (DateTime)oRetorno[0, 7];

            if (typeof(DBNull) != oRetorno[0, 8].GetType())
                this.DH_ALTERACAO = (DateTime)oRetorno[0, 8];

            this.TG_ESPECIAL = Convert.ToInt32(oRetorno[0, 9]);
            this.NR_DECIMAIS = Convert.ToInt32(oRetorno[0, 10]);
            this.FK_APPADRAO = Convert.ToInt32(oRetorno[0, 11]);
        }
    }
}
