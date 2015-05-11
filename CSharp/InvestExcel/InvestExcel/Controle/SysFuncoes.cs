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
    }
}
