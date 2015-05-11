using System;
using ExcelDna.Integration;
using System.Collections.Generic;
using System.Diagnostics;
using InvestExcel;
using SQL;
using InvestExcel.Classes;
using InvestExcel.Controle;

namespace InvestExcel
{
    public static class Publicado
    {
        private const string cCategoria = "Funções do Invest Publicador";

        [ExcelFunction(Name = "Invest.Inv.Nivel.Rentab.Lista", Category = cCategoria, Description = "Patrimonio de um Setor.")]
        public static object Invest_Inv_Nivel_Rentab_Lista(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cDe, Description = HelpText.cDe)]DateTime De,
            [ExcelArgument(Name = HelpName.cAte, Description = HelpText.cAte)]DateTime Ate,
            [ExcelArgument(Name = HelpName.cNivel, Description = HelpText.cNivel)]string Nivel,
            [ExcelArgument(Name = HelpName.cMoeda, Description = HelpText.cMoeda)]string Moeda)
        {
            object[,] oArg = ExcelDna.Params(Investimento);

            TCLUCARTCONSPERFIL oPerfil = new TCLUCARTCONSPERFIL(oArg);
            TCLUCARTCONSINDICE oInd = new TCLUCARTCONSINDICE(oArg, oPerfil.TG_NIVEL, oPerfil.TG_LCIBRUTA, oPerfil.TG_DESCONSIDERAR);

            QueryBuilder oCot = new QueryBuilder();

            if (oInd.PK_ID == 0)
                return ExcelEmpty.Value;

            oCot.AddCampo("COT.*");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", ">=", De);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "<=", Ate);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", Nivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            return ExcelDna.Return(oRetorno);
        }

        [ExcelFunction(Name = "Invest.Inv.Ativo.Rentab", Category = cCategoria, Description = "Patrimonio de um Setor.")]
        public static object Invest_Inv_Ativo_Rentab(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cAtivo, Description = HelpText.cAtivo)]string Ativo,
            [ExcelArgument(Name = HelpName.cDe, Description = HelpText.cDe)]DateTime De,
            [ExcelArgument(Name = HelpName.cAte, Description = HelpText.cAte)]DateTime Ate,
            [ExcelArgument(Name = HelpName.cMoeda, Description = HelpText.cMoeda)]string Moeda)
        {
            object[,] oArg = ExcelDna.Params(Investimento);

            TCLUCARTCONSPERFIL oPerfil = new TCLUCARTCONSPERFIL(oArg);
            TCLUCARTCONSINDICE oInd = new TCLUCARTCONSINDICE(oArg, oPerfil.TG_NIVEL, oPerfil.TG_LCIBRUTA, oPerfil.TG_DESCONSIDERAR);

            QueryBuilder oCot = new QueryBuilder();

            if(string.IsNullOrEmpty(Moeda))
                Moeda = "BRL";

            if (oInd.PK_ID == 0)
                return ExcelEmpty.Value;

            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao(1,"AND", "COT.DT_COTACAO", "=", De);
            oCot.AddCondicao(2,"OR", "COT.DT_COTACAO", "=", Ate);
            oCot.AddCondicao("AND", "COT.FK_ATIVO", "=", Ativo);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", "ATIVO");
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            double nRetorno = (Convert.ToDouble(oRetorno[1, 0]) / Convert.ToDouble(oRetorno[0, 0])) - 1;

            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.Ativo.Rentab.Lista", Category = cCategoria, Description = "Patrimonio de um Setor.")]
        public static object Invest_Inv_Ativo_Rentab_Lista(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cAtivo, Description = HelpText.cAtivo)]string Ativo,
            [ExcelArgument(Name = HelpName.cDe, Description = HelpText.cDe)]DateTime De,
            [ExcelArgument(Name = HelpName.cAte, Description = HelpText.cAte)]DateTime Ate,
            [ExcelArgument(Name = HelpName.cMoeda, Description = HelpText.cMoeda)]string Moeda)
        {
            object[,] oArg = ExcelDna.Params(Investimento);

            if (oArg[0, 0].ToString().IndexOf(":") > 0)
                return ExcelDna.Return(oArg);

            TCLUCARTCONSPERFIL oPerfil = new TCLUCARTCONSPERFIL(oArg);
            TCLUCARTCONSINDICE oInd = new TCLUCARTCONSINDICE(oArg, oPerfil.TG_NIVEL, oPerfil.TG_LCIBRUTA, oPerfil.TG_DESCONSIDERAR);

            QueryBuilder oCot = new QueryBuilder();

            if (string.IsNullOrEmpty(Moeda))
                Moeda = "BRL";

            if (oInd.PK_ID == 0)
                return ExcelEmpty.Value;

            oCot.AddCampo("COT.DT_COTACAO");
            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", ">=", De);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "<=", Ate);
            oCot.AddCondicao("AND", "COT.FK_ATIVO", "=", Ativo);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", "ATIVO");
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            return ExcelDna.Return(oRetorno);
        }


    }
}