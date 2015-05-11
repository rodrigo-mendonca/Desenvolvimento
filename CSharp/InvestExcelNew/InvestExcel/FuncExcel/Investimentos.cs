using System;
using ExcelDna.Integration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQL;
using InvestExcel.Classes;
using InvestExcel.Controle;

namespace InvestExcel
{
    public static class Investimentos
    {
        
        private const string cCategoria = "Informações do Investimento do Invest";

        [ExcelFunction(Name = "Invest.Inv.Perfil.Lista", Category = cCategoria, Description = "Cotação do Fundo Pragma.")]
        public static object Invest_Inv_Perfil(
            [ExcelArgument(AllowReference = true,Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]int Investimento)
        {
            object[,] oParams = ExcelDna.Params(Investimento);
            TCLUCARTCONSPERFIL oPerfil = new TCLUCARTCONSPERFIL(oParams);
            object[,] oRetorno = new object[3, 1];
            oRetorno[0, 0] = oPerfil.TG_NIVEL;
            oRetorno[1,0] = oPerfil.TG_DESCONSIDERAR;
            oRetorno[2,0] = oPerfil.TG_LCIBRUTA;

            return ExcelDna.Return(oRetorno);
        }

        [ExcelFunction(Name = "Invest.Inv.Cotacao", Category = cCategoria, Description = "Cotação do Fundo Pragma.")]
        public static Double Invest_Inv_Cotacao(
            [ExcelArgument(Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]int Investimento,
            [ExcelArgument(Name = HelpName.cData, Description = HelpText.cData)]DateTime Data)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("COALESCE(SUM(FEC.VL_COTASD0),0)", "COTACAO_ATIVO");
            oSql.AddTabela("TCLUFECHAMENTO", "FEC");
            oSql.AddCondicao("AND", "FEC.DT_ULTFECH", "=", Data);
            oSql.AddCondicao("AND", "USC.PK_ID", "=", Investimento);

            oSql.AddJoin("VCLUUSUCLUBE", "USC", "USC.PK_ID = FEC.FK_CLUBE");

            oSql.Executar();

            Double nRetorno = (double)oSql.Resultado();
            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.Dividendo", Category = cCategoria, Description = "Dividendos de um Fundo.")]
        public static Double Invest_Inv_Dividendo(
            [ExcelArgument(Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]int Investimento,
            [ExcelArgument(Name = HelpName.cData, Description = HelpText.cData)]DateTime Data)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("SUM(COALESCE(MOV.VL_DIVIDENDO,0))", "PL_PRAGMA");
            oSql.AddTabela("TCLUMOVCART", "MOV");
            oSql.AddCondicao("AND", "MOV.DT_MOVTO", "=", Data);
            oSql.AddCondicao("AND", "MOV.TG_CV", "=", "A");
            oSql.AddCondicao("AND", "USC.PK_ID", "=", Investimento);

            oSql.AddJoin("VCLUUSUCLUBE", "USC", "USC.PK_ID = MOV.FK_CLUBE");

            oSql.Executar();

            Double nRetorno = (double)oSql.Resultado();
            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.CPR", Category = cCategoria, Description = "Item de Contas a Pagar/Receber.")]
        public static Double Invest_Inv_CPR(
            [ExcelArgument(Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]int Investimento,
            [ExcelArgument(Name = HelpName.cData, Description = HelpText.cData)]DateTime Data)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("COALESCE(SUM(CPR.VL_PAGREC),0) ", "VL_PAGREC");
            oSql.AddTabela("TCLUPAGREC", "CPR");
            oSql.AddCondicao("AND", "CPR.FK_DESCLANCAMENTO", "=", 999);
            oSql.AddCondicao("AND", "CPR.DT_CARTEIRA", "=", Data);
            oSql.AddCondicao("AND", "USC.PK_ID", "=", Investimento);

            oSql.AddJoin("VCLUUSUCLUBE", "USC", "USC.PK_ID = CPR.FK_CLUBE");

            oSql.Executar();

            Double nRetorno = (Double)oSql.Resultado();
            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.TipoAtivo.Patrimonio", Category = cCategoria, Description = "Patrimonio de um Tipo Ativo.")]
        public static Double Invest_Inv_TipoAtivo_Patrimonio(
                [ExcelArgument(Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]int Investimento,
                [ExcelArgument(Name = HelpName.cTipoAtivo, Description = HelpText.cTipoAtivo)]int TipoAtivo,
                [ExcelArgument(Name = HelpName.cData, Description = HelpText.cData)]DateTime Data)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("COALESCE(SUM(CAR.VL_PATRIMONIO),0)", "VL_PATRIMONIOTIP");
            oSql.AddTabela("TCLUCARTEIRA", "CAR");
            oSql.AddCondicao("AND", "ATI.FK_TIPOATIVO", "=", TipoAtivo);
            oSql.AddCondicao("AND", "CAR.DT_CARTEIRA", "=", Data);
            oSql.AddCondicao("AND", "USC.PK_ID", "=", Investimento);

            oSql.AddJoin("VCLUUSUCLUBE", "USC", "USC.PK_ID = CAR.FK_CLUBE");
            oSql.AddJoin("TCLUATIVO", "ATI", "ATI.PK_ID = CAR.FK_ATIVO");

            oSql.Executar();

            Double nRetorno = (Double)oSql.Resultado();
            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.PL", Category = cCategoria, Description = "PL dros Fundos Pragma.")]
        public static Double Invest_Inv_PL(
            [ExcelArgument(Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]int Investimento,
            [ExcelArgument(Name = HelpName.cData, Description = HelpText.cData)]DateTime Data)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("COALESCE(FEC.VL_PLD0,0)", "PL_PRAGMA");
            oSql.AddTabela("TCLUFECHAMENTO", "FEC");
            oSql.AddCondicao("AND", "FEC.DT_ULTFECH", "=", Data);
            oSql.AddCondicao("AND", "USC.PK_ID", "=", Investimento);

            oSql.AddJoin("VCLUUSUCLUBE", "USC", "USC.PK_ID = FEC.FK_CLUBE");

            oSql.Executar();

            Double nRetorno = (Double)oSql.Resultado();
            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.PLBruto", Category = cCategoria, Description = "PL dos Fundos Pragma (Bruto).")]
        public static Double Invest_Inv_PLBruto(
            [ExcelArgument(Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]int Investimento,
            [ExcelArgument(Name = HelpName.cData, Description = HelpText.cData)]DateTime Data)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("COALESCE(FEC.VL_PLD0BRU,0)", "PL_PRAGMA");
            oSql.AddTabela("TCLUFECHAMENTO", "FEC");
            oSql.AddCondicao("AND", "FEC.DT_ULTFECH", "=", Data);
            oSql.AddCondicao("AND", "USC.PK_ID", "=", Investimento);

            oSql.AddJoin("VCLUUSUCLUBE", "USC", "USC.PK_ID = FEC.FK_CLUBE");

            oSql.Executar();

            Double nRetorno = (Double)oSql.Resultado();
            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.PLLocal", Category = cCategoria, Description = "PL Local do Fundo.")]
        public static Double Invest_Inv_PLLocal(
            [ExcelArgument(Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]int Investimento,
            [ExcelArgument(Name = HelpName.cData, Description = HelpText.cData)]DateTime Data)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("COALESCE(SUM(CAR.VL_PRETOT),0)", "PL_PRAGMA");
            oSql.AddTabela("TCLUCARTEIRA", "CAR");
            oSql.AddCondicao("AND", "CAR.DT_CARTEIRA", "=", Data);
            oSql.AddCondicao("AND", "USC.PK_ID", "=", Investimento);
            oSql.AddCondicao("AND", "CAR.TG_ESTRANGEIRO", "=", 0);

            oSql.AddJoin("VCLUUSUCLUBE", "USC", "USC.PK_ID = CAR.FK_CLUBE");

            oSql.Executar();

            Double nRetorno = (Double)oSql.Resultado();
            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.PLOff", Category = cCategoria, Description = "PL Off do Fundo.")]
        public static Double Invest_Inv_PLOff(
            [ExcelArgument(Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]int Investimento,
            [ExcelArgument(Name = HelpName.cData, Description = HelpText.cData)]DateTime Data)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("COALESCE(SUM(CAR.VL_PRETOT),0)", "PL_PRAGMA");
            oSql.AddTabela("TCLUCARTEIRA", "CAR");
            oSql.AddCondicao("AND", "CAR.DT_CARTEIRA", "=", Data);
            oSql.AddCondicao("AND", "USC.PK_ID", "=", Investimento);
            oSql.AddCondicao("AND", "CAR.TG_ESTRANGEIRO", "=", 1);

            oSql.AddJoin("VCLUUSUCLUBE", "USC", "USC.PK_ID = CAR.FK_CLUBE");

            oSql.Executar();

            Double nRetorno = (Double)oSql.Resultado();
            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.TipoAtivo.Quantidade", Category = cCategoria, Description = "Quantidade de um Fundo por Tipo Ativo.")]
        public static Double Invest_Inv_TipoAtivo_Quantidade(
                [ExcelArgument(Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]int Investimento,
                [ExcelArgument(Name = HelpName.cTipoAtivo, Description = HelpText.cTipoAtivo)]int TipoAtivo,
                [ExcelArgument(Name = HelpName.cData, Description = HelpText.cData)]DateTime Data)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("COALESCE(SUM(CAR.QT_CARTEIRA),0)", "QT_CARTEIRA");
            oSql.AddTabela("TCLUCARTEIRA", "CAR");
            oSql.AddCondicao("AND", "CAR.DT_CARTEIRA", "=", Data);
            oSql.AddCondicao("AND", "CAR.FK_TIPOATIVO", "=", TipoAtivo);
            oSql.AddCondicao("AND", "USC.PK_ID", "=", Investimento);

            oSql.AddJoin("VCLUUSUCLUBE", "USC", "USC.PK_ID = CAR.FK_CLUBE");

            oSql.Executar();

            Double nRetorno = (Double)oSql.Resultado();
            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.TxAdm", Category = cCategoria, Description = "Taxa ADM dos Fundos Pragma.")]
        public static Double Invest_Inv_TxAdm(
            [ExcelArgument(Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]int Investimento,
            [ExcelArgument(Name = HelpName.cData, Description = HelpText.cData)]DateTime Data)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("COALESCE(FEC.VL_TXADMD0,0)", "VL_TXADM");
            oSql.AddTabela("TCLUFECHAMENTO", "FEC");
            oSql.AddCondicao("AND", "FEC.DT_ULTFECH", "=", Data);
            oSql.AddCondicao("AND", "USC.PK_ID", "=", Investimento);

            oSql.AddJoin("VCLUUSUCLUBE", "USC", "USC.PK_ID = FEC.FK_CLUBE");

            oSql.Executar();

            Double nRetorno = (Double)oSql.Resultado();
            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.TxGes", Category = cCategoria, Description = "Taxa GES dos Fundos Pragma.")]
        public static Double Invest_Inv_TxGes(
            [ExcelArgument(Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]int Investimento,
            [ExcelArgument(Name = HelpName.cData, Description = HelpText.cData)]DateTime Data)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("COALESCE(FEC.VL_TXGESD0,0)", "VL_TXGES");
            oSql.AddTabela("TCLUFECHAMENTO", "FEC");
            oSql.AddCondicao("AND", "FEC.DT_ULTFECH", "=", Data);
            oSql.AddCondicao("AND", "USC.PK_ID", "=", Investimento);

            oSql.AddJoin("VCLUUSUCLUBE", "USC", "USC.PK_ID = FEC.FK_CLUBE");

            oSql.Executar();

            Double nRetorno = (Double)oSql.Resultado();
            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.Cotista.Quantidade", Category = cCategoria, Description = "Quantidade de cotas do cotista no investimento.")]
        public static double Invest_Inv_Cotista_Quantidade(
            [ExcelArgument(Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]int Investimento,
            [ExcelArgument(Name = HelpName.cData, Description = HelpText.cData)]DateTime Data,
            [ExcelArgument(Name = HelpName.cCotista, Description = HelpText.cCotista)]int Cotista)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("COT.QT_ATUAL", "QT_ATUAL");
            oSql.AddTabela("VCLUCOTSALDO", "COT");
            oSql.AddCondicao("AND", "COT.FK_CLUBE", "=", Investimento);
            oSql.AddCondicao("AND", "COT.DT_ATUAL", "=", Data);
            oSql.AddCondicao("AND", "COT.PK_ID", "=", Cotista);

            oSql.AddJoin("VCLUUSUCLUBE", "USC", "USC.PK_ID = COT.FK_CLUBE");
            oSql.Executar();

            double nRetorno = (double)oSql.Resultado();
            return nRetorno;
        }

        [ExcelFunction(Name = "Invest.Inv.TxFundo", Category = cCategoria, Description = "Valor de uma taxa dentro de um fundo.")]
        public static double Invest_Inv_TxFundo(
            [ExcelArgument(Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]int Investimento,
            [ExcelArgument(Name = HelpName.cData, Description = HelpText.cData)]DateTime Data,
            [ExcelArgument(Name = HelpName.cTaxa, Description = HelpText.cTaxa)]int CodTaxa)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("SUM(CPR.VL_PAGREC)", "VL_PAGREC");
            oSql.AddTabela("TCLUPAGREC", "CPR");

            oSql.AddCondicao("AND", "CPR.FK_CLUBE", "=", Investimento);
            oSql.AddCondicao("AND", "CPR.DT_CARTEIRA", "=", Data);
            oSql.AddCondicao("AND", "CPR.TG_PROVISAO", "<>", 1);
            oSql.AddCondicao("AND", "CPR.FK_DESCLANCAMENTO", "=", CodTaxa);

            oSql.AddJoin("VCLUUSUCLUBE", "USC", "USC.PK_ID = CPR.FK_CLUBE");

            oSql.Executar();

            double nRetorno = (double)oSql.Resultado();
            return nRetorno;
        }
    }
}
