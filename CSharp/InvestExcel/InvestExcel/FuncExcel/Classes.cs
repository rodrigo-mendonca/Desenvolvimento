using System;
using ExcelDna.Integration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQL;
using InvestExcel.Controle;
using InvestExcel.Classes;

namespace InvestExcel.FuncExcel
{
    public static class Classes
    {
        private const string cCategoria = "Funções do Invest Publicador";

        [ExcelFunction(Name = "Invest.Inv.Classe.Rentab", Category = cCategoria, Description = "Patrimonio de um Setor.")]
        public static object Invest_Inv_Classe_Rentab(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cClasse, Description = HelpText.cClasse)]string Classe,
            [ExcelArgument(Name = HelpName.cLocal, Description = HelpText.cLocal)]int Local,
            [ExcelArgument(Name = HelpName.cDe, Description = HelpText.cDe)]DateTime De,
            [ExcelArgument(Name = HelpName.cAte, Description = HelpText.cAte)]DateTime Ate,
            [ExcelArgument(Name = HelpName.cMoeda, Description = HelpText.cMoeda)]string Moeda)
        {
            object[,] oArg = ExcelDna.Params(Investimento);

            TCLUCARTCONSPERFIL oPerfil = new TCLUCARTCONSPERFIL(oArg);
            TCLUCARTCONSINDICE oInd = new TCLUCARTCONSINDICE(oArg, oPerfil.TG_NIVEL, oPerfil.TG_LCIBRUTA, oPerfil.TG_DESCONSIDERAR);

            QueryBuilder oCot = new QueryBuilder();

            if (string.IsNullOrEmpty(Moeda))
                Moeda = "BRL";

            if (oInd.PK_ID == 0)
                return ExcelEmpty.Value;

            string cNivel = "SETOR";
            if (Local == 2)
                cNivel += "CONS";

            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao(1, "AND", "COT.DT_COTACAO", "=", De);
            oCot.AddCondicao(2, "OR", "COT.DT_COTACAO", "=", Ate);
            oCot.AddCondicao("AND", "COT.FK_SETOR", "=", Classe);
            oCot.AddCondicao("AND", "COT.TG_ATIVOESTRANG", "=", Local);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            double nRetorno = (Convert.ToDouble(oRetorno[1, 0]) / Convert.ToDouble(oRetorno[0, 0])) - 1;

            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.Classe.Rentab.Lista", Category = cCategoria, Description = "Patrimonio de um Setor.")]
        public static object Invest_Inv_Classe_Rentab_Lista(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cClasse, Description = HelpText.cClasse)]string Classe,
            [ExcelArgument(Name = HelpName.cLocal, Description = HelpText.cLocal)]int Local,
            [ExcelArgument(Name = HelpName.cDe, Description = HelpText.cDe)]DateTime De,
            [ExcelArgument(Name = HelpName.cAte, Description = HelpText.cAte)]DateTime Ate,
            [ExcelArgument(Name = HelpName.cMoeda, Description = HelpText.cMoeda)]string Moeda)
        {
            object[,] oArg = ExcelDna.Params(Investimento);

            TCLUCARTCONSPERFIL oPerfil = new TCLUCARTCONSPERFIL(oArg);
            TCLUCARTCONSINDICE oInd = new TCLUCARTCONSINDICE(oArg, oPerfil.TG_NIVEL, oPerfil.TG_LCIBRUTA, oPerfil.TG_DESCONSIDERAR);

            QueryBuilder oCot = new QueryBuilder();

            if (string.IsNullOrEmpty(Moeda))
                Moeda = "BRL";

            if (oInd.PK_ID == 0)
                return ExcelEmpty.Value;

            string cNivel = "SETOR";
            if (Local == 2)
                cNivel += "CONS";

            oCot.AddCampo("COT.DT_COTACAO");
            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", ">=", De);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "<=", Ate);
            oCot.AddCondicao("AND", "COT.FK_SETOR", "=", Classe);
            oCot.AddCondicao("AND", "COT.TG_ATIVOESTRANG", "=", Local);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            return ExcelDna.Return(oRetorno);
        }

        [ExcelFunction(Name = "Invest.Inv.Classe.Patrimonio", Category = cCategoria, Description = "Patrimonio de um Setor.")]
        public static object Invest_Inv_Classe_Rentab_Lista(
            [ExcelArgument(Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]int Investimento,
            [ExcelArgument(Name = HelpName.cClasse, Description = HelpText.cClasse)]string Classe,
            [ExcelArgument(Name = HelpName.cData, Description = HelpText.cData)]DateTime Data)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("COALESCE(SUM(CAR.VL_PATRIMONIO),0)", "VL_PATRIMONIOSET");
            oSql.AddTabela("TCLUCARTEIRA", "CAR");
            oSql.AddCondicao("AND","ATI.FK_SETOR", "=", Classe);
            oSql.AddCondicao("AND", "CAR.DT_CARTEIRA", "=", Data);
            oSql.AddCondicao("AND", "USC.PK_ID", "=", Investimento);

            oSql.AddJoin("VCLUUSUCLUBE", "USC", "USC.PK_ID = CAR.FK_CLUBE");
            oSql.AddJoin("TCLUATIVO", "ATI", "ATI.PK_ID = CAR.FK_ATIVO");

            oSql.Executar();
            return oSql.Resultado();;
        }
   }
}
