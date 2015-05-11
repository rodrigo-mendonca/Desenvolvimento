using System;
using ExcelDna.Integration;
using System.IO;
using SQL;
namespace InvestExcel
{
    public static class InvestExcel
    {
        private const string cCategoria = "InvestDebug";

        [ExcelFunction(Category = "InvestDebug", Description = "Retorna versão da Xll.")]
        public static string DebugVersao()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        [ExcelFunction(Category = "InvestDebug", Description = "Retorna usuario da base de dados.")]
        public static string DebugUsuDb()
        {
            return ("teste1");
        }
    }
}