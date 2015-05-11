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

        #region ATIVO

        [ExcelFunction(Name = "Invest.Inv.Ativo.DtInicio", Category = cCategoria, Description = "Menor data de um periodo para um ativo.")]
        public static object Invest_Inv_Ativo_DtInicio(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cAtivo, Description = HelpText.cAtivo)]string Ativo,
            [ExcelArgument(Name = HelpName.cVencimento, Description = HelpText.cVencimento)]string Vencimento,
            [ExcelArgument(Name = HelpName.cOpcao, Description = HelpText.cOpcao)]string Opcao,
            [ExcelArgument(Name = HelpName.cInternoGestor, Description = HelpText.cInternoGestor)]string InternoGestor,
            [ExcelArgument(Name = HelpName.cDe, Description = HelpText.cDe)]DateTime De,
            [ExcelArgument(Name = HelpName.cAte, Description = HelpText.cAte)]DateTime Ate)
        {
            object[,] oArg = ExcelDna.Params(Investimento);

            TCLUCARTCONSPERFIL oPerfil = new TCLUCARTCONSPERFIL(oArg);
            TCLUCARTCONSINDICE oInd = new TCLUCARTCONSINDICE(oArg, oPerfil.TG_NIVEL, oPerfil.TG_LCIBRUTA, oPerfil.TG_DESCONSIDERAR);

            QueryBuilder oCot = new QueryBuilder();

            if (oInd.PK_ID == 0)
                return ExcelEmpty.Value;

            oCot.AddCampo("MIN(COT.DT_COTACAO)", "DT_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", ">=", De);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "<=", Ate);
            oCot.AddCondicao("AND", "COT.FK_ATIVO", "=", Ativo);
            oCot.AddCondicao("AND", "COT.DS_VENCIMENTO", "=", Vencimento);
            oCot.AddCondicao("AND", "COT.CD_OPCAO", "=", Opcao);
            oCot.AddCondicao("AND", "COT.CD_INTERNOGESTOR", "=", InternoGestor);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", "ATIVO");
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());
            DateTime dRetorno = Convert.ToDateTime(oRetorno[0, 0]);
            return dRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.Ativo.Rentab", Category = cCategoria, Description = "Renabilidade de um periodo para um ativo.")]
        public static object Invest_Inv_Ativo_Rentab(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cAtivo, Description = HelpText.cAtivo)]string Ativo,
            [ExcelArgument(Name = HelpName.cVencimento, Description = HelpText.cVencimento)]string Vencimento,
            [ExcelArgument(Name = HelpName.cOpcao, Description = HelpText.cOpcao)]string Opcao,
            [ExcelArgument(Name = HelpName.cInternoGestor, Description = HelpText.cInternoGestor)]string InternoGestor,
            [ExcelArgument(Name = HelpName.cDe, Description = HelpText.cDe)]DateTime De,
            [ExcelArgument(Name = HelpName.cAte, Description = HelpText.cAte)]DateTime Ate,
            [ExcelArgument(Name = HelpName.cMoeda, Description = HelpText.cMoeda)]string Moeda)
        {
            object[,] oArg = ExcelDna.Params(Investimento);

            TCLUCARTCONSPERFIL oPerfil = new TCLUCARTCONSPERFIL(oArg);
            TCLUCARTCONSINDICE oInd = new TCLUCARTCONSINDICE(oArg, oPerfil.TG_NIVEL, oPerfil.TG_LCIBRUTA, oPerfil.TG_DESCONSIDERAR);

            QueryBuilder oCot = new QueryBuilder();

            if (oInd.PK_ID == 0)
                return ExcelEmpty.Value;

            if (string.IsNullOrEmpty(Moeda))
                Moeda = "BRL";

            // BUSCA A MENOR DATA DO ATIVO
            DateTime dIni = Convert.ToDateTime(Invest_Inv_Ativo_DtInicio(Investimento, Ativo, Vencimento, Opcao, InternoGestor, De, Ate));

            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao(1, "AND", "COT.DT_COTACAO", "=", dIni);
            oCot.AddCondicao(2,"OR", "COT.DT_COTACAO", "=", Ate);
            oCot.AddCondicao("AND", "COT.FK_ATIVO", "=", Ativo);
            oCot.AddCondicao("AND", "COT.DS_VENCIMENTO", "=", Vencimento);
            oCot.AddCondicao("AND", "COT.CD_OPCAO", "=", Opcao);
            oCot.AddCondicao("AND", "COT.CD_INTERNOGESTOR", "=", InternoGestor);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", "ATIVO");
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            double nRetorno = (Convert.ToDouble(oRetorno[1, 0]) / Convert.ToDouble(oRetorno[0, 0])) - 1;

            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.Ativo.Rentab.Lista", Category = cCategoria, Description = "Lista de renabilidades de um periodo para um ativo.")]
        public static object Invest_Inv_Ativo_Rentab_Lista(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cAtivo, Description = HelpText.cAtivo)]string Ativo,
            [ExcelArgument(Name = HelpName.cVencimento, Description = HelpText.cVencimento)]string Vencimento,
            [ExcelArgument(Name = HelpName.cOpcao, Description = HelpText.cOpcao)]string Opcao,
            [ExcelArgument(Name = HelpName.cInternoGestor, Description = HelpText.cInternoGestor)]string InternoGestor,
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
            oCot.AddCondicao("AND", "COT.DS_VENCIMENTO", "=", Vencimento);
            oCot.AddCondicao("AND", "COT.CD_OPCAO", "=", Opcao);
            oCot.AddCondicao("AND", "COT.CD_INTERNOGESTOR", "=", InternoGestor);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", "ATIVO");
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = SysFuncoes.CalcRentab(oCot.ResultadoMatriz());
            return ExcelDna.Return(oRetorno);
        }

        [ExcelFunction(Name = "Invest.Inv.Ativo.Patrimonio", Category = cCategoria, Description = "Patrimiônio de um periodo para um ativo.")]
        public static object Invest_Inv_Ativo_Patrimonio(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cAtivo, Description = HelpText.cAtivo)]string Ativo,
            [ExcelArgument(Name = HelpName.cVencimento, Description = HelpText.cVencimento)]string Vencimento,
            [ExcelArgument(Name = HelpName.cOpcao, Description = HelpText.cOpcao)]string Opcao,
            [ExcelArgument(Name = HelpName.cInternoGestor, Description = HelpText.cInternoGestor)]string InternoGestor,
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

            oCot.AddCampo("COT.VL_PATRIMONIO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "=", Ate);
            oCot.AddCondicao("AND", "COT.FK_ATIVO", "=", Ativo);
            oCot.AddCondicao("AND", "COT.DS_VENCIMENTO", "=", Vencimento);
            oCot.AddCondicao("AND", "COT.CD_OPCAO", "=", Opcao);
            oCot.AddCondicao("AND", "COT.CD_INTERNOGESTOR", "=", InternoGestor);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", "ATIVO");
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            double nRetorno = Convert.ToDouble(oRetorno[0, 0]);

            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.Ativo.Patrimonio.Lista", Category = cCategoria, Description = "Lista de patrimiônios de um periodo para um ativo.")]
        public static object Invest_Inv_Ativo_Patrimonio_Lista(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cAtivo, Description = HelpText.cAtivo)]string Ativo,
            [ExcelArgument(Name = HelpName.cVencimento, Description = HelpText.cVencimento)]string Vencimento,
            [ExcelArgument(Name = HelpName.cOpcao, Description = HelpText.cOpcao)]string Opcao,
            [ExcelArgument(Name = HelpName.cInternoGestor, Description = HelpText.cInternoGestor)]string InternoGestor,
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

            oCot.AddCampo("COT.DT_COTACAO");
            oCot.AddCampo("COT.VL_PATRIMONIO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", ">=", De);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "<=", Ate);
            oCot.AddCondicao("AND", "COT.FK_ATIVO", "=", Ativo);
            oCot.AddCondicao("AND", "COT.DS_VENCIMENTO", "=", Vencimento);
            oCot.AddCondicao("AND", "COT.CD_OPCAO", "=", Opcao);
            oCot.AddCondicao("AND", "COT.CD_INTERNOGESTOR", "=", InternoGestor);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", "ATIVO");
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());
            return oRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.Ativo.Cota", Category = cCategoria, Description = "Cota de um periodo para um ativo.")]
        public static object Invest_Inv_Ativo_Cota(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cAtivo, Description = HelpText.cAtivo)]string Ativo,
            [ExcelArgument(Name = HelpName.cVencimento, Description = HelpText.cVencimento)]string Vencimento,
            [ExcelArgument(Name = HelpName.cOpcao, Description = HelpText.cOpcao)]string Opcao,
            [ExcelArgument(Name = HelpName.cInternoGestor, Description = HelpText.cInternoGestor)]string InternoGestor,
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

            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "=", Ate);
            oCot.AddCondicao("AND", "COT.FK_ATIVO", "=", Ativo);
            oCot.AddCondicao("AND", "COT.DS_VENCIMENTO", "=", Vencimento);
            oCot.AddCondicao("AND", "COT.CD_OPCAO", "=", Opcao);
            oCot.AddCondicao("AND", "COT.CD_INTERNOGESTOR", "=", InternoGestor);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", "ATIVO");
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            double nRetorno = Convert.ToDouble(oRetorno[0, 0]);

            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.Ativo.Cota.Lista", Category = cCategoria, Description = "Lista de cotas de um periodo para um ativo.")]
        public static object Invest_Inv_Ativo_Cota_Lista(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cAtivo, Description = HelpText.cAtivo)]string Ativo,
            [ExcelArgument(Name = HelpName.cVencimento, Description = HelpText.cVencimento)]string Vencimento,
            [ExcelArgument(Name = HelpName.cOpcao, Description = HelpText.cOpcao)]string Opcao,
            [ExcelArgument(Name = HelpName.cInternoGestor, Description = HelpText.cInternoGestor)]string InternoGestor,
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

            oCot.AddCampo("COT.DT_COTACAO");
            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", ">=", De);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "<=", Ate);
            oCot.AddCondicao("AND", "COT.FK_ATIVO", "=", Ativo);
            oCot.AddCondicao("AND", "COT.DS_VENCIMENTO", "=", Vencimento);
            oCot.AddCondicao("AND", "COT.CD_OPCAO", "=", Opcao);
            oCot.AddCondicao("AND", "COT.CD_INTERNOGESTOR", "=", InternoGestor);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", "ATIVO");
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());
            return oRetorno;
        }
        #endregion

        #region SUBCLASSE

        [ExcelFunction(Name = "Invest.Inv.SubClasse.DtInicio", Category = cCategoria, Description = "Lista de rentabilidades publicadas da sub.classe.")]
        public static object Invest_Inv_SubClasse_DtInicio(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cSubClasse, Description = HelpText.cSubClasse)]int SubClasse,
            [ExcelArgument(Name = HelpName.cClasse, Description = HelpText.cClasse)]int Classe,
            [ExcelArgument(Name = HelpName.cDesConsiderar, Description = HelpText.cDesConsiderar)]int DesConsiderar,
            [ExcelArgument(Name = HelpName.cLocal, Description = HelpText.cLocal)]int Local,
            [ExcelArgument(Name = HelpName.cDe, Description = HelpText.cDe)]DateTime De,
            [ExcelArgument(Name = HelpName.cAte, Description = HelpText.cAte)]DateTime Ate)
        {
            object[,] oArg = ExcelDna.Params(Investimento);

            TCLUCARTCONSPERFIL oPerfil = new TCLUCARTCONSPERFIL(oArg);
            TCLUCARTCONSINDICE oInd = new TCLUCARTCONSINDICE(oArg, oPerfil.TG_NIVEL, oPerfil.TG_LCIBRUTA, oPerfil.TG_DESCONSIDERAR);

            QueryBuilder oCot = new QueryBuilder();

            if (oInd.PK_ID == 0)
                return ExcelEmpty.Value;

            string cNivel = "CLASSE";

            oCot.AddCampo("MIN(COT.DT_COTACAO)", "DT_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", ">=", De);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "<=", Ate);
            oCot.AddCondicao("AND", "COT.FK_CLASSE", "=", SubClasse);
            oCot.AddCondicao("AND", "COT.FK_SETOR", "=", Classe);
            oCot.AddCondicao("AND", "COT.TG_DESCONSIDERAR", "=", DesConsiderar);
            oCot.AddCondicao("AND", "COT.TG_ATIVOESTRANG", "=", Local);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());
            DateTime dRetorno = Convert.ToDateTime(oRetorno[0, 0]);
            return dRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.SubClasse.Rentab", Category = cCategoria, Description = "Rentabilidade publicada da sub.classe.")]
        public static object Invest_Inv_SubClasse_Rentab(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cSubClasse, Description = HelpText.cSubClasse)]int SubClasse,
            [ExcelArgument(Name = HelpName.cClasse, Description = HelpText.cClasse)]int Classe,
            [ExcelArgument(Name = HelpName.cDesConsiderar, Description = HelpText.cDesConsiderar)]int DesConsiderar,
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

            string cNivel = "CLASSE";
            // BUSCA A MENOR DATA DA SUBCLASSE
            DateTime dIni = Convert.ToDateTime(Invest_Inv_SubClasse_DtInicio(Investimento, SubClasse, Classe, DesConsiderar, Local, De, Ate));

            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao(1, "AND", "COT.DT_COTACAO", "=", dIni);
            oCot.AddCondicao(2, "OR", "COT.DT_COTACAO", "=", Ate);
            oCot.AddCondicao("AND", "COT.FK_CLASSE", "=", SubClasse);
            oCot.AddCondicao("AND", "COT.FK_SETOR", "=", Classe);
            oCot.AddCondicao("AND", "COT.TG_DESCONSIDERAR", "=", DesConsiderar);
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

        [ExcelFunction(Name = "Invest.Inv.SubClasse.Rentab.Lista", Category = cCategoria, Description = "Lista de rentabilidades publicadas da sub.classe.")]
        public static object Invest_Inv_SubClasse_Rentab_Lista(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cSubClasse, Description = HelpText.cSubClasse)]int SubClasse,
            [ExcelArgument(Name = HelpName.cClasse, Description = HelpText.cClasse)]int Classe,
            [ExcelArgument(Name = HelpName.cDesConsiderar, Description = HelpText.cDesConsiderar)]int DesConsiderar,
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

            string cNivel = "CLASSE";

            oCot.AddCampo("COT.DT_COTACAO");
            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", ">=", De);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "<=", Ate);
            oCot.AddCondicao("AND", "COT.FK_CLASSE", "=", SubClasse);
            oCot.AddCondicao("AND", "COT.FK_SETOR", "=", Classe);
            oCot.AddCondicao("AND", "COT.TG_DESCONSIDERAR", "=", DesConsiderar);
            oCot.AddCondicao("AND", "COT.TG_ATIVOESTRANG", "=", Local);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = SysFuncoes.CalcRentab(oCot.ResultadoMatriz());
            return ExcelDna.Return(oRetorno);
        }

        [ExcelFunction(Name = "Invest.Inv.SubClasse.Patrimonio", Category = cCategoria, Description = "Patrimônio publicado da sub.classe.")]
        public static object Invest_Inv_SubClasse_Patrimonio(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cSubClasse, Description = HelpText.cSubClasse)]int SubClasse,
            [ExcelArgument(Name = HelpName.cClasse, Description = HelpText.cClasse)]int Classe,
            [ExcelArgument(Name = HelpName.cDesConsiderar, Description = HelpText.cDesConsiderar)]int DesConsiderar,
            [ExcelArgument(Name = HelpName.cLocal, Description = HelpText.cLocal)]int Local,
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

            string cNivel = "CLASSE";

            oCot.AddCampo("COT.VL_PATRIMONIO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "=", Ate);
            oCot.AddCondicao("AND", "COT.FK_CLASSE", "=", SubClasse);
            oCot.AddCondicao("AND", "COT.FK_SETOR", "=", Classe);
            oCot.AddCondicao("AND", "COT.TG_DESCONSIDERAR", "=", DesConsiderar);
            oCot.AddCondicao("AND", "COT.TG_ATIVOESTRANG", "=", Local);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            double nRetorno = Convert.ToDouble(oRetorno[0, 0]);

            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.SubClasse.Patrimonio.Lista", Category = cCategoria, Description = "Lista de patrimônios publicado da sub.classe.")]
        public static object Invest_Inv_SubClasse_Patrimonio_Lista(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cSubClasse, Description = HelpText.cSubClasse)]int SubClasse,
            [ExcelArgument(Name = HelpName.cClasse, Description = HelpText.cClasse)]int Classe,
            [ExcelArgument(Name = HelpName.cDesConsiderar, Description = HelpText.cDesConsiderar)]int DesConsiderar,
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

            string cNivel = "CLASSE";

            oCot.AddCampo("COT.DT_COTACAO");
            oCot.AddCampo("COT.VL_PATRIMONIO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", ">=", De);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "<=", Ate);
            oCot.AddCondicao("AND", "COT.FK_CLASSE", "=", SubClasse);
            oCot.AddCondicao("AND", "COT.FK_SETOR", "=", Classe);
            oCot.AddCondicao("AND", "COT.TG_DESCONSIDERAR", "=", DesConsiderar);
            oCot.AddCondicao("AND", "COT.TG_ATIVOESTRANG", "=", Local);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            return ExcelDna.Return(oRetorno);
        }


        [ExcelFunction(Name = "Invest.Inv.SubClasse.Cota", Category = cCategoria, Description = "Cota publicada da sub.classe.")]
        public static object Invest_Inv_SubClasse_Cota(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cSubClasse, Description = HelpText.cSubClasse)]int SubClasse,
            [ExcelArgument(Name = HelpName.cClasse, Description = HelpText.cClasse)]int Classe,
            [ExcelArgument(Name = HelpName.cDesConsiderar, Description = HelpText.cDesConsiderar)]int DesConsiderar,
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

            string cNivel = "CLASSE";

            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "=", Ate);
            oCot.AddCondicao("AND", "COT.FK_CLASSE", "=", SubClasse);
            oCot.AddCondicao("AND", "COT.FK_SETOR", "=", Classe);
            oCot.AddCondicao("AND", "COT.TG_DESCONSIDERAR", "=", DesConsiderar);
            oCot.AddCondicao("AND", "COT.TG_ATIVOESTRANG", "=", Local);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            double nRetorno = Convert.ToDouble(oRetorno[0, 0]);

            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.SubClasse.Cota.Lista", Category = cCategoria, Description = "Lista cotas publicadas da sub.classe.")]
        public static object Invest_Inv_SubClasse_Cota_Lista(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cSubClasse, Description = HelpText.cSubClasse)]int SubClasse,
            [ExcelArgument(Name = HelpName.cClasse, Description = HelpText.cClasse)]int Classe,
            [ExcelArgument(Name = HelpName.cDesConsiderar, Description = HelpText.cDesConsiderar)]int DesConsiderar,
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

            string cNivel = "CLASSE";

            oCot.AddCampo("COT.DT_COTACAO");
            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", ">=", De);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "<=", Ate);
            oCot.AddCondicao("AND", "COT.FK_CLASSE", "=", SubClasse);
            oCot.AddCondicao("AND", "COT.FK_SETOR", "=", Classe);
            oCot.AddCondicao("AND", "COT.TG_DESCONSIDERAR", "=", DesConsiderar);
            oCot.AddCondicao("AND", "COT.TG_ATIVOESTRANG", "=", Local);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            return ExcelDna.Return(oRetorno);
        }
        #endregion

        #region CLASSE

        [ExcelFunction(Name = "Invest.Inv.Classe.DtInicio", Category = cCategoria, Description = "Lista de rentabilidades publicadas da classe.")]
        public static object Invest_Inv_Classe_DtInicio(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cClasse, Description = HelpText.cClasse)]int Classe,
            [ExcelArgument(Name = HelpName.cDesConsiderar, Description = HelpText.cDesConsiderar)]int DesConsiderar,
            [ExcelArgument(Name = HelpName.cLocal, Description = HelpText.cLocal)]int Local,
            [ExcelArgument(Name = HelpName.cDe, Description = HelpText.cDe)]DateTime De,
            [ExcelArgument(Name = HelpName.cAte, Description = HelpText.cAte)]DateTime Ate)
        {
            object[,] oArg = ExcelDna.Params(Investimento);

            TCLUCARTCONSPERFIL oPerfil = new TCLUCARTCONSPERFIL(oArg);
            TCLUCARTCONSINDICE oInd = new TCLUCARTCONSINDICE(oArg, oPerfil.TG_NIVEL, oPerfil.TG_LCIBRUTA, oPerfil.TG_DESCONSIDERAR);

            QueryBuilder oCot = new QueryBuilder();
            if (oInd.PK_ID == 0)
                return ExcelEmpty.Value;

            string cNivel = "SETOR";
            if (Local == 2)
                cNivel += "CONS";

            oCot.AddCampo("MIN(COT.DT_COTACAO)", "DT_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", ">=", De);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "<=", Ate);
            oCot.AddCondicao("AND", "COT.FK_SETOR", "=", Classe);
            oCot.AddCondicao("AND", "COT.TG_DESCONSIDERAR", "=", DesConsiderar);
            oCot.AddCondicao("AND", "COT.TG_ATIVOESTRANG", "=", Local);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());
            DateTime dRetorno = Convert.ToDateTime(oRetorno[0, 0]);
            return dRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.Classe.Rentab", Category = cCategoria, Description = "Rentabilidade publicada da classe.")]
        public static object Invest_Inv_Classe_Rentab(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cClasse, Description = HelpText.cClasse)]int Classe,
            [ExcelArgument(Name = HelpName.cDesConsiderar, Description = HelpText.cDesConsiderar)]int DesConsiderar,
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

            // BUSCA A MENOR DATA DA CLASSE
            DateTime dIni = Convert.ToDateTime(Invest_Inv_Classe_DtInicio(Investimento, Classe, DesConsiderar, Local, De, Ate));

            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao(1, "AND", "COT.DT_COTACAO", "=", dIni);
            oCot.AddCondicao(2, "OR", "COT.DT_COTACAO", "=", Ate);
            oCot.AddCondicao("AND", "COT.FK_SETOR", "=", Classe);
            oCot.AddCondicao("AND", "COT.TG_DESCONSIDERAR", "=", DesConsiderar);
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

        [ExcelFunction(Name = "Invest.Inv.Classe.Rentab.Lista", Category = cCategoria, Description = "Lista de rentabilidades publicadas da classe.")]
        public static object Invest_Inv_Classe_Rentab_Lista(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cClasse, Description = HelpText.cClasse)]string Classe,
            [ExcelArgument(Name = HelpName.cDesConsiderar, Description = HelpText.cDesConsiderar)]int DesConsiderar,
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
            oCot.AddCondicao("AND", "COT.TG_DESCONSIDERAR", "=", DesConsiderar);
            oCot.AddCondicao("AND", "COT.TG_ATIVOESTRANG", "=", Local);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = SysFuncoes.CalcRentab(oCot.ResultadoMatriz());
            return ExcelDna.Return(oRetorno);
        }

        [ExcelFunction(Name = "Invest.Inv.Classe.Patrimonio", Category = cCategoria, Description = "Patrimônio publicado da classe.")]
        public static object Invest_Inv_Classe_Patrimonio(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cClasse, Description = HelpText.cClasse)]string Classe,
            [ExcelArgument(Name = HelpName.cDesConsiderar, Description = HelpText.cDesConsiderar)]int DesConsiderar,
            [ExcelArgument(Name = HelpName.cLocal, Description = HelpText.cLocal)]int Local,
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

            oCot.AddCampo("COT.VL_PATRIMONIO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "=", Ate);
            oCot.AddCondicao("AND", "COT.FK_SETOR", "=", Classe);
            oCot.AddCondicao("AND", "COT.TG_DESCONSIDERAR", "=", DesConsiderar);
            oCot.AddCondicao("AND", "COT.TG_ATIVOESTRANG", "=", Local);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            double nRetorno = Convert.ToDouble(oRetorno[0, 0]);

            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.Classe.Patrimonio.Lista", Category = cCategoria, Description = "Lista de patrimônios publicado da classe.")]
        public static object Invest_Inv_Classe_Patrimonio_Lista(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cClasse, Description = HelpText.cClasse)]string Classe,
            [ExcelArgument(Name = HelpName.cDesConsiderar, Description = HelpText.cDesConsiderar)]int DesConsiderar,
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
            oCot.AddCampo("COT.VL_PATRIMONIO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", ">=", De);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "<=", Ate);
            oCot.AddCondicao("AND", "COT.FK_SETOR", "=", Classe);
            oCot.AddCondicao("AND", "COT.TG_DESCONSIDERAR", "=", DesConsiderar);
            oCot.AddCondicao("AND", "COT.TG_ATIVOESTRANG", "=", Local);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            return ExcelDna.Return(oRetorno);
        }

        [ExcelFunction(Name = "Invest.Inv.Classe.Cota", Category = cCategoria, Description = "Cota publicada da classe.")]
        public static object Invest_Inv_Classe_Cota(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cClasse, Description = HelpText.cClasse)]string Classe,
            [ExcelArgument(Name = HelpName.cDesConsiderar, Description = HelpText.cDesConsiderar)]int DesConsiderar,
            [ExcelArgument(Name = HelpName.cLocal, Description = HelpText.cLocal)]int Local,
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
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "=", Ate);
            oCot.AddCondicao("AND", "COT.FK_SETOR", "=", Classe);
            oCot.AddCondicao("AND", "COT.TG_DESCONSIDERAR", "=", DesConsiderar);
            oCot.AddCondicao("AND", "COT.TG_ATIVOESTRANG", "=", Local);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            double nRetorno = Convert.ToDouble(oRetorno[0, 0]);

            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.Classe.Cota.Lista", Category = cCategoria, Description = "Lista de cotas publicada da classe.")]
        public static object Invest_Inv_Classe_Cota_Lista(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cClasse, Description = HelpText.cClasse)]string Classe,
            [ExcelArgument(Name = HelpName.cDesConsiderar, Description = HelpText.cDesConsiderar)]int DesConsiderar,
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
            oCot.AddCondicao("AND", "COT.TG_DESCONSIDERAR", "=", DesConsiderar);
            oCot.AddCondicao("AND", "COT.TG_ATIVOESTRANG", "=", Local);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            return ExcelDna.Return(oRetorno);
        }
        #endregion

        #region DESCONSIDERAR
        [ExcelFunction(Name = "Invest.Inv.Desconsiderar.DtInicio", Category = cCategoria, Description = "Lista de rentabilidades publicadas dos ativo considerados ou desconsiderados.")]
        public static object Invest_Inv_Desconsiderar_DtInicio(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cDesConsiderar, Description = HelpText.cDesConsiderar)]int DesConsiderar,
            [ExcelArgument(Name = HelpName.cLocal, Description = HelpText.cLocal)]int Local,
            [ExcelArgument(Name = HelpName.cDe, Description = HelpText.cDe)]DateTime De,
            [ExcelArgument(Name = HelpName.cAte, Description = HelpText.cAte)]DateTime Ate)
        {
            object[,] oArg = ExcelDna.Params(Investimento);

            TCLUCARTCONSPERFIL oPerfil = new TCLUCARTCONSPERFIL(oArg);
            TCLUCARTCONSINDICE oInd = new TCLUCARTCONSINDICE(oArg, oPerfil.TG_NIVEL, oPerfil.TG_LCIBRUTA, oPerfil.TG_DESCONSIDERAR);

            QueryBuilder oCot = new QueryBuilder();

            if (oInd.PK_ID == 0)
                return ExcelEmpty.Value;

            string cNivel = "DESCONSIDERAR";

            oCot.AddCampo("MIN(COT.DT_COTACAO)", "DT_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", ">=", De);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "<=", Ate);
            oCot.AddCondicao("AND", "COT.TG_DESCONSIDERAR", "=", DesConsiderar);
            oCot.AddCondicao("AND", "COT.TG_ATIVOESTRANG", "=", Local);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());
            DateTime dRetorno = Convert.ToDateTime(oRetorno[0, 0]);
            return dRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.Desconsiderar.Rentab", Category = cCategoria, Description = "Rentabilidade publicada dos ativo considerados ou desconsiderados.")]
        public static object Invest_Inv_Desconsiderar_Rentab(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cDesConsiderar, Description = HelpText.cDesConsiderar)]int DesConsiderar,
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

            string cNivel = "DESCONSIDERAR";

            // BUSCA A MENOR DATA
            DateTime dIni = Convert.ToDateTime(Invest_Inv_Desconsiderar_DtInicio(Investimento, DesConsiderar, Local, De, Ate));

            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao(1, "AND", "COT.DT_COTACAO", "=", dIni);
            oCot.AddCondicao(2, "OR", "COT.DT_COTACAO", "=", Ate);
            oCot.AddCondicao("AND", "COT.TG_DESCONSIDERAR", "=", DesConsiderar);
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

        [ExcelFunction(Name = "Invest.Inv.Desconsiderar.Rentab.Lista", Category = cCategoria, Description = "Lista de rentabilidades publicadas dos ativo considerados ou desconsiderados.")]
        public static object Invest_Inv_Desconsiderar_Rentab_Lista(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cDesConsiderar, Description = HelpText.cDesConsiderar)]int DesConsiderar,
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

            string cNivel = "DESCONSIDERAR";

            oCot.AddCampo("COT.DT_COTACAO");
            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", ">=", De);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "<=", Ate);
            oCot.AddCondicao("AND", "COT.TG_DESCONSIDERAR", "=", DesConsiderar);
            oCot.AddCondicao("AND", "COT.TG_ATIVOESTRANG", "=", Local);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = SysFuncoes.CalcRentab(oCot.ResultadoMatriz());
            return ExcelDna.Return(oRetorno);
        }

        [ExcelFunction(Name = "Invest.Inv.Desconsiderar.Patrimonio", Category = cCategoria, Description = "Patrimônio publicado dos ativo considerados ou desconsiderados.")]
        public static object Invest_Inv_Desconsiderar_Patrimonio(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cDesConsiderar, Description = HelpText.cDesConsiderar)]int DesConsiderar,
            [ExcelArgument(Name = HelpName.cLocal, Description = HelpText.cLocal)]int Local,
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

            string cNivel = "DESCONSIDERAR";

            oCot.AddCampo("COT.VL_PATRIMONIO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "=", Ate);
            oCot.AddCondicao("AND", "COT.TG_DESCONSIDERAR", "=", DesConsiderar);
            oCot.AddCondicao("AND", "COT.TG_ATIVOESTRANG", "=", Local);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            double nRetorno = Convert.ToDouble(oRetorno[0, 0]);

            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.Desconsiderar.Patrimonio.Lista", Category = cCategoria, Description = "Lista de patrimônios publicado dos ativo considerados ou desconsiderados.")]
        public static object Invest_Inv_Desconsiderar_Patrimonio_Lista(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cDesConsiderar, Description = HelpText.cDesConsiderar)]int DesConsiderar,
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

            string cNivel = "DESCONSIDERAR";

            oCot.AddCampo("COT.DT_COTACAO");
            oCot.AddCampo("COT.VL_PATRIMONIO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", ">=", De);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "<=", Ate);
            oCot.AddCondicao("AND", "COT.TG_DESCONSIDERAR", "=", DesConsiderar);
            oCot.AddCondicao("AND", "COT.TG_ATIVOESTRANG", "=", Local);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            return ExcelDna.Return(oRetorno);
        }

        [ExcelFunction(Name = "Invest.Inv.Desconsiderar.Cota", Category = cCategoria, Description = "Cota publicada dos ativo considerados ou desconsiderados.")]
        public static object Invest_Inv_Desconsiderar_Cota(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cDesConsiderar, Description = HelpText.cDesConsiderar)]int DesConsiderar,
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

            string cNivel = "DESCONSIDERAR";

            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "=", Ate);
            oCot.AddCondicao("AND", "COT.TG_DESCONSIDERAR", "=", DesConsiderar);
            oCot.AddCondicao("AND", "COT.TG_ATIVOESTRANG", "=", Local);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            double nRetorno = Convert.ToDouble(oRetorno[0, 0]);

            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.Desconsiderar.Cota.Lista", Category = cCategoria, Description = "Lista de cotas publicada dos ativo considerados ou desconsiderados.")]
        public static object Invest_Inv_Desconsiderar_Cota_Lista(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cDesConsiderar, Description = HelpText.cDesConsiderar)]int DesConsiderar,
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

            string cNivel = "DESCONSIDERAR";

            oCot.AddCampo("COT.DT_COTACAO");
            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", ">=", De);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "<=", Ate);
            oCot.AddCondicao("AND", "COT.TG_DESCONSIDERAR", "=", DesConsiderar);
            oCot.AddCondicao("AND", "COT.TG_ATIVOESTRANG", "=", Local);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            return ExcelDna.Return(oRetorno);
        }
        #endregion

        #region LOCAL
        [ExcelFunction(Name = "Invest.Inv.Local.DtInicio", Category = cCategoria, Description = "Lista de rentabilidades publicada local ou internacional.")]
        public static object Invest_Inv_Local_DtInicio(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cLocal, Description = HelpText.cLocal)]int Local,
            [ExcelArgument(Name = HelpName.cDe, Description = HelpText.cDe)]DateTime De,
            [ExcelArgument(Name = HelpName.cAte, Description = HelpText.cAte)]DateTime Ate)
        {
            object[,] oArg = ExcelDna.Params(Investimento);

            TCLUCARTCONSPERFIL oPerfil = new TCLUCARTCONSPERFIL(oArg);
            TCLUCARTCONSINDICE oInd = new TCLUCARTCONSINDICE(oArg, oPerfil.TG_NIVEL, oPerfil.TG_LCIBRUTA, oPerfil.TG_DESCONSIDERAR);

            QueryBuilder oCot = new QueryBuilder();

            if (oInd.PK_ID == 0)
                return ExcelEmpty.Value;

            string cNivel = "LOCAL";

            oCot.AddCampo("MIN(COT.DT_COTACAO)", "DT_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", ">=", De);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "<=", Ate);
            oCot.AddCondicao("AND", "COT.TG_ATIVOESTRANG", "=", Local);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());
            DateTime dRetorno = Convert.ToDateTime(oRetorno[0, 0]);
            return dRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.Local.Rentab", Category = cCategoria, Description = "Rentabilidade publicada local ou internacional.")]
        public static object Invest_Inv_Local_Rentab(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
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

            string cNivel = "LOCAL";
            // BUSCA A MENOR DATA
            DateTime dIni = Convert.ToDateTime(Invest_Inv_Local_DtInicio(Investimento, Local, De, Ate));

            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao(1, "AND", "COT.DT_COTACAO", "=", De);
            oCot.AddCondicao(2, "OR", "COT.DT_COTACAO", "=", Ate);
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

        [ExcelFunction(Name = "Invest.Inv.Local.Rentab.Lista", Category = cCategoria, Description = "Lista de rentabilidades publicada local ou internacional.")]
        public static object Invest_Inv_Local_Rentab_Lista(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
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

            string cNivel = "LOCAL";

            oCot.AddCampo("COT.DT_COTACAO");
            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", ">=", De);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "<=", Ate);
            oCot.AddCondicao("AND", "COT.TG_ATIVOESTRANG", "=", Local);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = SysFuncoes.CalcRentab(oCot.ResultadoMatriz());
            return ExcelDna.Return(oRetorno);
        }

        [ExcelFunction(Name = "Invest.Inv.Local.Patrimonio", Category = cCategoria, Description = "Patrimônio publicado local ou internacional.")]
        public static object Invest_Inv_Local_Patrimonio(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cLocal, Description = HelpText.cLocal)]int Local,
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

            string cNivel = "LOCAL";

            oCot.AddCampo("COT.VL_PATRIMONIO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "=", Ate);
            oCot.AddCondicao("AND", "COT.TG_ATIVOESTRANG", "=", Local);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            double nRetorno = Convert.ToDouble(oRetorno[0, 0]);

            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.Local.Patrimonio.Lista", Category = cCategoria, Description = "Lista de patrimônios publicado local ou internacional.")]
        public static object Invest_Inv_Local_Patrimonio_Lista(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
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

            string cNivel = "LOCAL";

            oCot.AddCampo("COT.DT_COTACAO");
            oCot.AddCampo("COT.VL_PATRIMONIO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", ">=", De);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "<=", Ate);
            oCot.AddCondicao("AND", "COT.TG_ATIVOESTRANG", "=", Local);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            return ExcelDna.Return(oRetorno);
        }


        [ExcelFunction(Name = "Invest.Inv.Local.Cota", Category = cCategoria, Description = "Cota publicada local ou internacional.")]
        public static object Invest_Inv_Local_Cota(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
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

            string cNivel = "LOCAL";

            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "=", Ate);
            oCot.AddCondicao("AND", "COT.TG_ATIVOESTRANG", "=", Local);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            double nRetorno = Convert.ToDouble(oRetorno[0, 0]);

            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.Local.Cota.Lista", Category = cCategoria, Description = "Lista cotas publicada local ou internacional.")]
        public static object Invest_Inv_Local_Cota_Lista(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
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

            string cNivel = "LOCAL";

            oCot.AddCampo("COT.DT_COTACAO");
            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", ">=", De);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "<=", Ate);
            oCot.AddCondicao("AND", "COT.TG_ATIVOESTRANG", "=", Local);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            return ExcelDna.Return(oRetorno);
        }
        #endregion

        #region CONSOLIDADO
        [ExcelFunction(Name = "Invest.Inv.Consolidado.DtInicio", Category = cCategoria, Description = "Lista de rentabilidades publicadas do investimento.")]
        public static object Invest_Inv_Consolidado_DtInicio(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cDe, Description = HelpText.cDe)]DateTime De,
            [ExcelArgument(Name = HelpName.cAte, Description = HelpText.cAte)]DateTime Ate)
        {
            object[,] oArg = ExcelDna.Params(Investimento);

            TCLUCARTCONSPERFIL oPerfil = new TCLUCARTCONSPERFIL(oArg);
            TCLUCARTCONSINDICE oInd = new TCLUCARTCONSINDICE(oArg, oPerfil.TG_NIVEL, oPerfil.TG_LCIBRUTA, oPerfil.TG_DESCONSIDERAR);

            QueryBuilder oCot = new QueryBuilder();

            if (oInd.PK_ID == 0)
                return ExcelEmpty.Value;

            string cNivel = "CONSOLIDADO";

            oCot.AddCampo("MIN(COT.DT_COTACAO)", "DT_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", ">=", De);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "<=", Ate);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());
            DateTime dRetorno = Convert.ToDateTime(oRetorno[0, 0]);
            return dRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.Consolidado.Rentab", Category = cCategoria, Description = "Rentabilidade publicada do investimento.")]
        public static object Invest_Inv_Consolidado_Rentab(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
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

            string cNivel = "CONSOLIDADO";
            // BUSCA A MENOR DATA
            DateTime dIni = Convert.ToDateTime(Invest_Inv_Consolidado_DtInicio(Investimento, De, Ate));

            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao(1, "AND", "COT.DT_COTACAO", "=", De);
            oCot.AddCondicao(2, "OR", "COT.DT_COTACAO", "=", Ate);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            double nRetorno = (Convert.ToDouble(oRetorno[1, 0]) / Convert.ToDouble(oRetorno[0, 0])) - 1;

            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.Consolidado.Rentab.Lista", Category = cCategoria, Description = "Lista de rentabilidades publicadas do investimento.")]
        public static object Invest_Inv_Consolidado_Rentab_Lista(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
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

            string cNivel = "CONSOLIDADO";

            oCot.AddCampo("COT.DT_COTACAO");
            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", ">=", De);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "<=", Ate);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = SysFuncoes.CalcRentab(oCot.ResultadoMatriz());
            return ExcelDna.Return(oRetorno);
        }

        [ExcelFunction(Name = "Invest.Inv.Consolidado.Patrimonio", Category = cCategoria, Description = "Patrimônio publicado do Investimento.")]
        public static object Invest_Inv_Consolidado_Patrimonio(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
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

            string cNivel = "CONSOLIDADO";

            oCot.AddCampo("COT.VL_PATRIMONIO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "=", Ate);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            double nRetorno = Convert.ToDouble(oRetorno[0, 0]);

            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.Consolidado.Patrimonio.Lista", Category = cCategoria, Description = "Lista de patrimônios publicado do Investimento.")]
        public static object Invest_Inv_Consolidado_Patrimonio_Lista(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
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

            string cNivel = "CONSOLIDADO";

            oCot.AddCampo("COT.DT_COTACAO");
            oCot.AddCampo("COT.VL_PATRIMONIO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", ">=", De);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "<=", Ate);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            return ExcelDna.Return(oRetorno);
        }

        [ExcelFunction(Name = "Invest.Inv.Consolidado.Cota", Category = cCategoria, Description = "Cota publicada do Investimento.")]
        public static object Invest_Inv_Consolidado_Cota(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
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

            string cNivel = "CONSOLIDADO";

            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "=", Ate);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            double nRetorno = Convert.ToDouble(oRetorno[0, 0]);

            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.Consolidado.Cota.Lista", Category = cCategoria, Description = "Lista de cotas publicada do Investimento.")]
        public static object Invest_Inv_Consolidado_Cota_Lista(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
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

            string cNivel = "CONSOLIDADO";

            oCot.AddCampo("COT.DT_COTACAO");
            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", ">=", De);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "<=", Ate);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            return ExcelDna.Return(oRetorno);
        }
        #endregion

        #region NIVEL

        [ExcelFunction(Name = "Invest.Inv.Nivel.Rentab", Category = cCategoria, Description = "Rentabilidade publicada de um nivel.")]
        public static object Invest_Inv_Nivel_Rentab(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cNivel, Description = HelpText.cNivel)]string Nivel,
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

            string cNivel = Nivel.Trim().ToUpper();

            oCot.AddCampo("COT.DT_COTACAO");
            oCot = SysFuncoes.CamposNivel(oCot, cNivel);
            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao(1, "AND", "COT.DT_COTACAO", "=", De);
            oCot.AddCondicao(2, "OR", "COT.DT_COTACAO", "=", Ate);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,]  oRetorno = SysFuncoes.CalcRentabNivel(De, Ate, oCot.ResultadoMatriz());
            return ExcelDna.Return(oRetorno);
        }
        
        [ExcelFunction(Name = "Invest.Inv.Nivel.Patrimonio", Category = cCategoria, Description = "Patrimonio de um nivel.")]
        public static object Invest_Inv_Nivel_Patrimonio(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cNivel, Description = HelpText.cNivel)]string Nivel,
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

            string cNivel = Nivel.Trim().ToUpper();

            oCot = SysFuncoes.CamposNivel(oCot, cNivel);
            oCot.AddCampo("COT.VL_PATRIMONIO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "=", Ate);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            return ExcelDna.Return(oRetorno);
        }

        [ExcelFunction(Name = "Invest.Inv.Nivel.Patrimonio.Lista", Category = cCategoria, Description = "Lista de patrimonios de um nivel.")]
        public static object Invest_Inv_Nivel_Patrimonio_Lista(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cNivel, Description = HelpText.cNivel)]string Nivel,
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

            string cNivel = Nivel.Trim().ToUpper();

            oCot.AddCampo("COT.DT_COTACAO");
            oCot = SysFuncoes.CamposNivel(oCot, cNivel);
            oCot.AddCampo("COT.VL_PATRIMONIO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", ">=", De);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "<=", Ate);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            return ExcelDna.Return(oRetorno);
        }

        [ExcelFunction(Name = "Invest.Inv.Nivel.Cota", Category = cCategoria, Description = "Cota de um nivel.")]
        public static object Invest_Inv_Nivel_Cota(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cNivel, Description = HelpText.cNivel)]string Nivel,
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

            string cNivel = Nivel.Trim().ToUpper();

            oCot = SysFuncoes.CamposNivel(oCot, cNivel);
            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "=", Ate);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            return ExcelDna.Return(oRetorno);
        }

        [ExcelFunction(Name = "Invest.Inv.Nivel.Cota.Lista", Category = cCategoria, Description = "Lista de Cotas de um nivel.")]
        public static object Invest_Inv_Nivel_Cota_Lista(
            [ExcelArgument(AllowReference = true, Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]object Investimento,
            [ExcelArgument(Name = HelpName.cNivel, Description = HelpText.cNivel)]string Nivel,
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

            string cNivel = Nivel.Trim().ToUpper();

            oCot.AddCampo("COT.DT_COTACAO");
            oCot = SysFuncoes.CamposNivel(oCot, cNivel);
            oCot.AddCampo("COT.VL_COTACAO");
            oCot.AddTabela("TCLUCARTCONSCOTA", "COT");
            oCot.AddCondicao("AND", "COT.FK_CARTCONS", "=", oInd.PK_ID);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", ">=", De);
            oCot.AddCondicao("AND", "COT.DT_COTACAO", "<=", Ate);
            oCot.AddCondicao("AND", "COT.DS_NIVEL", "=", cNivel);
            oCot.AddCondicao("AND", "COT.FK_MOEDA", "=", Moeda);
            oCot.AddCondicao("AND", "COT.DS_EXTRA", "=", "");
            oCot.AddOrder("COT.DT_COTACAO");
            oCot.Executar();

            object[,] oRetorno = ExcelDna.ListToMatriz(oCot.ResultadoMatriz());

            return ExcelDna.Return(oRetorno);
        }

        #endregion
    }
}