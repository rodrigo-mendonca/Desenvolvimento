using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQL;

namespace InvestExcel
{
    public static class SysFuncoes
    {
        public static object[,] RedimMatriz(object[,] tObj,int tRow,int tCol)
        {
            if (tRow == 0 || tCol == 0)
                return new object[1, 1];

            object[,] oRetorno = new object[tRow, tCol];

            for (int i = 0; i < tRow; i++)
            {
                for (int j = 0; j < tCol; j++)
                {
                    oRetorno[i, j] = tObj[i, j];
                }
            }

            return oRetorno;
        }


        public static string MontarIn(object[,] oObjs)
        {
            int[] oList = MatrizToVetorInt(oObjs);

            string cRetorno = "";
            for (int i = 0; i < oList.Length; i++)
            {
                if (oList[i].ToString() != "")
                {
                    if(Convert.ToInt32(oList[i]) != 0)
                        cRetorno += "," + Convert.ToInt32(oList[i]).ToString("0000");
                }
            }
            return cRetorno.Substring(1);
        }

        public static int[] MatrizToVetorInt(object[,] oObjs)
        {
            int c = 0;
            int nLen = (oObjs.GetLength(0)+oObjs.GetLength(1))-1;

            int[] oRetorno = new int[nLen];

            for (int i = 0; i < oObjs.GetLength(0); i++)
            {
                for (int j = 0; j < oObjs.GetLength(1); j++)
                {
                    if(typeof(string) != oObjs[i, j].GetType())
                        oRetorno[c] = Convert.ToInt32(oObjs[i, j]);
                    c++;
                }
            }
            return oRetorno;
        }

        public static string[] MatrizToVetorString(object[,] oObjs,string cFormat)
        {
            int c = 0;
            int nLen = (oObjs.GetLength(0) + oObjs.GetLength(1)) - 1;

            string[] oRetorno = new string[nLen];

            for (int i = 0; i < oObjs.GetLength(0); i++)
            {
                for (int j = 0; j < oObjs.GetLength(1); j++)
                {
                    if (typeof(string) != oObjs[i, j].GetType())
                    {
                        if (cFormat == "")
                            oRetorno[c] = oObjs[i, j].ToString();
                        else
                            oRetorno[c] = Convert.ToInt32(oObjs[i, j]).ToString(cFormat);
                    }
                    else
                        oRetorno[c] = oObjs[i, j].ToString();

                    c++;
                }
            }
            return oRetorno;
        }

        public static int[] OrdenaVetorInt(int[] oObjs)
        {
            int nAux;
            for (int i = 0; i < oObjs.Length; i++)
            {
                for (int j = (i+1); j < oObjs.Length; j++)
                {
                    if (oObjs[i] > oObjs[j])
                    {
                        nAux = oObjs[i];
                        oObjs[i] = oObjs[j];
                        oObjs[j] = nAux;
                    }
                }
            }
            return oObjs;
        }

        public static bool VerifDireitoClube(object[,] tInv)
        {
            string[] cListInvestimento = SysFuncoes.MatrizToVetorString(tInv, "0000");

            QueryBuilder oQuery = new QueryBuilder();

            oQuery.AddCampo("COUNT(1)", "NR_DIREITO");
            oQuery.AddTabela("VCLUUSUCLUBE", "USU");
            oQuery.AddCondicaoIn("USU.PK_ID", cListInvestimento);
            oQuery.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oQuery.ResultadoMatriz());

            if (Convert.ToInt16(oRetorno[0,0]) == 0)
                return false;

            return true;
        }

        public static object[,] CalcRentab(List<List<object>> tCotas)
        {
            List<List<object>> oCotas = new List<List<object>>();
            int nInd = 0,nIndAnt = 0;
            object oAnt;

            foreach (List<object> oI in tCotas)
            {
                // ignora o primeiro dia
                if (tCotas.IndexOf(oI) == 0)
                    continue;
                
                List<object> oLinha = new List<object>();
                nIndAnt = tCotas.IndexOf(oI) - 1;
                
                foreach (object oJ in oI)
                {
                    nInd = oI.IndexOf(oJ);
                    // busca o dia anterior
                    oAnt = tCotas[nIndAnt][nInd];
                    // se for a ultima coluna, a ultima coluna é da cota
                    if (nInd == (oI.Count - 1))
                        oLinha.Add(Convert.ToDouble(oJ) / Convert.ToDouble(oAnt) - 1);
                    else
                        oLinha.Add(oJ);
                }
                oCotas.Add(oLinha);
            }
            // converte a list para matriz
            object[,] oRetorno = ExcelDna.ListToMatriz(oCotas);
            return (oRetorno);
        }

        public static object[,] CalcRentabNivel(DateTime dDe, DateTime dAte, List<List<object>> tCotas)
        {
            List<List<object>> oCotas = new List<List<object>>();
            int nInd = 0;
            object oAnt = 0;

            foreach (List<object> oI in tCotas)
            {
                // ignora o primeiro dia
                if (Convert.ToDateTime(oI[0]) == dDe)
                {
                    oAnt = oI[oI.Count - 1];
                    continue;
                }

                List<object> oLinha = new List<object>();
                foreach (object oJ in oI)
                {
                    nInd = oI.IndexOf(oJ);
                    // se for a ultima coluna, a ultima coluna é da cota
                    if (nInd == (oI.Count - 1))
                    {
                        if (Convert.ToDouble(oAnt) == 0)
                            oLinha.Add((Convert.ToDouble(oJ) -100)/100);
                        else
                            oLinha.Add((Convert.ToDouble(oJ) / Convert.ToDouble(oAnt) - 1));
                        oAnt = 0;
                    }
                    else
                        oLinha.Add(oJ);
                }
                oCotas.Add(oLinha);
            }
            // converte a list para matriz
            object[,] oRetorno = ExcelDna.ListToMatriz(oCotas);
            return (oRetorno);
        }

        public static QueryBuilder CamposNivel(QueryBuilder tQuery,string tNivel)
        {
            switch (tNivel)
            {
                case "LOCAL":
                    tQuery.AddCampo("CASE COT.TG_ATIVOESTRANG WHEN 0 THEN 'LOCAL' ELSE 'INTERNACIONAL' END AS DS_LOCAL");
                    tQuery.AddOrder("COT.TG_ATIVOESTRANG");
                    break;
                case "DESCONSIDERAR":
                    tQuery.AddCampo("COT.TG_ATIVOESTRANG");
                    tQuery.AddCampo("COT.TG_DESCONSIDERAR");

                    tQuery.AddOrder("COT.TG_ATIVOESTRANG");
                    tQuery.AddOrder("COT.TG_DESCONSIDERAR");
                    break;
                case "SETOR":
                    tQuery.AddCampo("COT.TG_ATIVOESTRANG");
                    tQuery.AddCampo("COT.TG_DESCONSIDERAR");
                    tQuery.AddCampo("COT.FK_SETOR");

                    tQuery.AddOrder("COT.TG_ATIVOESTRANG");
                    tQuery.AddOrder("COT.TG_DESCONSIDERAR");
                    tQuery.AddOrder("COT.FK_SETOR");
                    break;
                case "CLASSE":
                    tQuery.AddCampo("COT.TG_ATIVOESTRANG");
                    tQuery.AddCampo("COT.TG_DESCONSIDERAR");
                    tQuery.AddCampo("COT.FK_SETOR");
                    tQuery.AddCampo("COT.FK_CLASSE");

                    tQuery.AddOrder("COT.TG_ATIVOESTRANG");
                    tQuery.AddOrder("COT.TG_DESCONSIDERAR");
                    tQuery.AddOrder("COT.FK_SETOR");
                    tQuery.AddOrder("COT.FK_CLASSE");
                    break;
                case "CONJUNTO":
                    tQuery.AddCampo("COT.TG_ATIVOESTRANG");
                    tQuery.AddCampo("COT.TG_DESCONSIDERAR");
                    tQuery.AddCampo("COT.FK_SETOR");
                    tQuery.AddCampo("COT.FK_CLASSE");
                    tQuery.AddCampo("COT.FK_CONJUNTO");

                    tQuery.AddOrder("COT.TG_ATIVOESTRANG");
                    tQuery.AddOrder("COT.TG_DESCONSIDERAR");
                    tQuery.AddOrder("COT.FK_SETOR");
                    tQuery.AddOrder("COT.FK_CLASSE");
                    tQuery.AddOrder("COT.FK_CONJUNTO");
                    break;
                case "ATIVO":
                    tQuery.AddCampo("COT.TG_ATIVOESTRANG");
                    tQuery.AddCampo("COT.TG_DESCONSIDERAR");
                    tQuery.AddCampo("COT.FK_SETOR");
                    tQuery.AddCampo("COT.FK_CLASSE");
                    tQuery.AddCampo("COT.FK_CONJUNTO");
                    tQuery.AddCampo("COT.FK_ATIVO");
                    tQuery.AddCampo("COT.DS_VENCIMENTO");
                    tQuery.AddCampo("COT.CD_INTERNOGESTOR");
                    tQuery.AddCampo("COT.CD_OPCAO");

                    tQuery.AddOrder("COT.TG_ATIVOESTRANG");
                    tQuery.AddOrder("COT.TG_DESCONSIDERAR");
                    tQuery.AddOrder("COT.FK_SETOR");
                    tQuery.AddOrder("COT.FK_CLASSE");
                    tQuery.AddOrder("COT.FK_CONJUNTO");
                    tQuery.AddOrder("COT.FK_ATIVO");
                    tQuery.AddOrder("COT.DS_VENCIMENTO");
                    tQuery.AddOrder("COT.CD_INTERNOGESTOR");
                    tQuery.AddOrder("COT.CD_OPCAO");
                    break;
            }
            return tQuery;
        }
    }
}
