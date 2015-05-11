using System;
using ExcelDna.Integration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQL;
using InvestExcel.Controle;

namespace InvestExcel
{
    public static class Ativos
    {
        private const string cCategoria = "Informações dos ativos do Invest.";
       
        [ExcelFunction(Name = "Invest.Inv.Ativo.Cotacao", Category = cCategoria, Description = "Cotação do Ativo na Carteira.")]
        public static object Invest_Inv_Ativo_Cotacao(
            [ExcelArgument(Name = HelpName.cAtivo, Description = HelpText.cAtivo)]string Ativo,
            [ExcelArgument(Name = HelpName.cData, Description = HelpText.cData)]DateTime Data,
            [ExcelArgument(Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]int Investimento)
        {
            QueryBuilder oSql = new QueryBuilder();
            
            oSql.AddTop(1);
            oSql.AddCampo("COALESCE(SUM(CAR.VL_COTACAO),0)", "COTACAO_ATIVO");
            oSql.AddTabela("TCLUCARTEIRA", "CAR");
            oSql.AddCondicao("AND","CAR.FK_ATIVO", "=", Ativo);
            oSql.AddCondicao("AND","CAR.DT_CARTEIRA", "=", Data);
            oSql.AddCondicao("AND", "USC.PK_ID", "=", Investimento);

            oSql.AddJoin("VCLUUSUCLUBE", "USC", "USC.PK_ID = CAR.FK_CLUBE");
            oSql.Executar();

            return oSql.Resultado();
        }

        [ExcelFunction(Name = "Invest.Ativo.Cotacao", Category = cCategoria, Description = "Cotação de um Ativo ou índice.")]
        public static object Invest_Ativo_Cotacao(
            [ExcelArgument(Name = HelpName.cAtivo, Description = HelpText.cAtivo)]string Ativo,
            [ExcelArgument(Name = HelpName.cData, Description = HelpText.cData)]DateTime Data)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("COALESCE(SUM(COT.VL_PREFEC),0)", "COTACAO_ATIVO");
            oSql.AddTabela("TCLUCOTACAO", "COT");
            oSql.AddCondicao("AND", "COT.FK_ATIVO", "=", Ativo);
            oSql.AddCondicao("AND", "COT.DT_COTACAO", "=", Data);

            oSql.Executar();

            return oSql.Resultado();
        }

        [ExcelFunction(Name = "Invest.Inv.Ativo.Lci.Cotacao", Category = cCategoria, Description = "Cotação do LCI/LCA/CRI na Carteira.")]
        public static object Invest_Inv_Ativo_LCI_Cotacao(
            [ExcelArgument(Name = HelpName.cAtivo, Description = HelpText.cAtivo)]string Ativo,
            [ExcelArgument(Name = HelpName.cData, Description = HelpText.cData)]DateTime Data,
            [ExcelArgument(Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]int Investimento)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("COALESCE(SUM(CAR.VL_COTACAO),0)", "COTACAO_ATIVO");
            oSql.AddTabela("TCLUCARTEIRA", "CAR");
            oSql.AddCondicao("AND", "CAR.DS_VENCIMENTO", "=", Ativo);
            oSql.AddCondicao("AND", "CAR.DT_CARTEIRA", "=", Data);
            oSql.AddCondicao("AND", "USC.PK_ID", "=", Investimento);

            oSql.AddJoin("VCLUUSUCLUBE", "USC", "USC.PK_ID = CAR.FK_CLUBE");

            oSql.Executar();

            return oSql.Resultado();
        }

        [ExcelFunction(Name = "Invest.Inv.Ativo.Dividendo", Category = cCategoria, Description = "Dividendo Pago do Ativo.")]
        public static object Invest_Inv_Ativo_Dividendo(
            [ExcelArgument(Name = HelpName.cAtivo, Description = HelpText.cAtivo)]string Ativo,
            [ExcelArgument(Name = HelpName.cData, Description = HelpText.cData)]DateTime Data,
            [ExcelArgument(Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]int Investimento)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("COALESCE(CAR.VL_AJUSTE1,0)", "VL_DIVIDENDO");
            oSql.AddTabela("TCLUCARTEIRA", "CAR");
            oSql.AddCondicao("AND", "CAR.FK_ATIVO", "=", Ativo);
            oSql.AddCondicao("AND", "CAR.DT_CARTEIRA", "=", Data);
            oSql.AddCondicao("AND", "USC.PK_ID", "=", Investimento);

            oSql.AddJoin("VCLUUSUCLUBE", "USC", "USC.PK_ID = CAR.FK_CLUBE");

            oSql.Executar();

            return oSql.Resultado();
        }

        [ExcelFunction(Name = "Invest.Ativo.Isin", Category = cCategoria, Description = "Isin do Cadastro do Ativo.")]
        public static object Invest_Ativo_Isin(
            [ExcelArgument(Name = HelpName.cAtivo, Description = HelpText.cAtivo)]string Ativo)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("COALESCE(ATI.CD_ISIN,'')", "CD_ISIN");
            oSql.AddTabela("TCLUATIVO", "ATI");
            oSql.AddCondicao("AND", "ATI.PK_ID", "=", Ativo);

            oSql.Executar();

            return oSql.Resultado();
        }

        [ExcelFunction(Name = "Invest.Ativo.Moeda", Category = cCategoria, Description = "Moeda do Cadastro do Ativo.")]
        public static object Invest_Ativo_Moeda(
            [ExcelArgument(Name = HelpName.cAtivo, Description = HelpText.cAtivo)]string Ativo)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("COALESCE(ATI.FK_MOEDAESTRANGEIRA,'')", "DS_MOEDA");
            oSql.AddTabela("TCLUATIVO", "ATI");
            oSql.AddCondicao("AND", "ATI.PK_ID", "=", Ativo);

            oSql.Executar();

            return oSql.Resultado();
        }

        [ExcelFunction(Name = "Invest.Inv.Ativo.Movto", Category = cCategoria, Description = "Movimento do Ativo na Carteira.")]
        public static object Invest_Inv_Ativo_Movto(
            [ExcelArgument(Name = HelpName.cAtivo, Description = HelpText.cAtivo)]string Ativo,
            [ExcelArgument(Name = HelpName.cData, Description = HelpText.cData)]DateTime Data,
            [ExcelArgument(Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]int Investimento)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("COALESCE(CAR.VL_MOVIMENTOANT,0)", "VL_MOVTO");
            oSql.AddTabela("TCLUCARTEIRA", "CAR");
            oSql.AddCondicao("AND", "CAR.FK_ATIVO", "=", Ativo);
            oSql.AddCondicao("AND", "CAR.DT_CARTEIRA", "=", Data);
            oSql.AddCondicao("AND", "USC.PK_ID", "=", Investimento);

            oSql.AddJoin("VCLUUSUCLUBE", "USC", "USC.PK_ID = CAR.FK_CLUBE");

            oSql.Executar();

            return oSql.Resultado();
        }

        [ExcelFunction(Name = "Invest.Inv.Ativo.Lci.Patrimonio", Category = cCategoria, Description = "PL dos Ativos LCI Pragma.")]
        public static object Invest_Inv_Ativo_LCI_Patrimonio(
            [ExcelArgument(Name = HelpName.cAtivo, Description = HelpText.cAtivo)]string Ativo,
            [ExcelArgument(Name = HelpName.cData, Description = HelpText.cData)]DateTime Data,
            [ExcelArgument(Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]int Investimento)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("COALESCE(SUM(CAR.VL_PATRIMONIO),0)", "VL_PATRIMONIOTIP");
            oSql.AddTabela("TCLUCARTEIRA", "CAR");
            oSql.AddCondicao("AND", "CAR.DS_VENCIMENTO", "=", Ativo);
            oSql.AddCondicao("AND", "CAR.DT_CARTEIRA", "=", Data);
            oSql.AddCondicao("AND", "USC.PK_ID", "=", Investimento);

            oSql.AddJoin("VCLUUSUCLUBE", "USC", "USC.PK_ID = CAR.FK_CLUBE");

            oSql.Executar();

            return oSql.Resultado();
        }

        [ExcelFunction(Name = "Invest.Inv.Ativo.Mercado", Category = cCategoria, Description = "PL dos Ativos Pragma (Mercado).")]
        public static object Invest_Inv_Ativo_Mercado(
            [ExcelArgument(Name = HelpName.cAtivo, Description = HelpText.cAtivo)]string Ativo,
            [ExcelArgument(Name = HelpName.cData, Description = HelpText.cData)]DateTime Data,
            [ExcelArgument(Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]int Investimento)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("COALESCE(SUM(CAR.VL_PRETOT),0) ", "PL_PRAGMA");
            oSql.AddTabela("TCLUCARTEIRA", "CAR");
            oSql.AddCondicao("AND", "CAR.FK_ATIVO", "=", Ativo);
            oSql.AddCondicao("AND", "CAR.DT_CARTEIRA", "=", Data);
            oSql.AddCondicao("AND", "USC.PK_ID", "=", Investimento);

            oSql.AddJoin("VCLUUSUCLUBE", "USC", "USC.PK_ID = CAR.FK_CLUBE");

            oSql.Executar();

            return oSql.Resultado();
        }

        [ExcelFunction(Name = "Invest.Fundo.PL", Category = cCategoria, Description = "PL Total dos Fundos Terceiros.")]
        public static object Invest_Fundo_PL(
            [ExcelArgument(Name = HelpName.cAtivo, Description = HelpText.cAtivo)]string Ativo,
            [ExcelArgument(Name = HelpName.cData, Description = HelpText.cData)]DateTime Data)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("COALESCE(ATI.VL_PL,0)", "PL_REAL");
            oSql.AddTabela("TCLUCOTFUNDO", "ATI");
            oSql.AddCondicao("AND", "ATI.DT_COTACAO", "=", Data);
            oSql.AddCondicao("AND", "ATI.FK_ATIVO", "=", Ativo);

            oSql.Executar();

            return oSql.Resultado();
        }

        [ExcelFunction(Name = "Invest.Inv.Ativo.Quantidade", Category = cCategoria, Description = "Quantidade do Ativo na Carteira.")]
        public static object Invest_Inv_Ativo_Quantidade(
            [ExcelArgument(Name = HelpName.cAtivo, Description = HelpText.cAtivo)]string Ativo,
            [ExcelArgument(Name = HelpName.cData, Description = HelpText.cData)]DateTime Data,
            [ExcelArgument(Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]int Investimento)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("COALESCE(SUM(CAR.QT_CARTEIRA),0)", "QT_ATIVO");
            oSql.AddTabela("TCLUCARTEIRA", "CAR");
            oSql.AddCondicao("AND", "CAR.DT_CARTEIRA", "=", Data);
            oSql.AddCondicao("AND", "CAR.FK_ATIVO", "=", Ativo);
            oSql.AddCondicao("AND", "USC.PK_ID", "=", Investimento);

            oSql.AddJoin("VCLUUSUCLUBE", "USC", "USC.PK_ID = CAR.FK_CLUBE");

            oSql.Executar();

            return oSql.Resultado();
        }

        [ExcelFunction(Name = "Invest.Inv.Ativo.Principal", Category = cCategoria, Description = "Valor Principal do Ativo.")]
        public static object Invest_Inv_Ativo_Principal(
            [ExcelArgument(Name = HelpName.cAtivo, Description = HelpText.cAtivo)]string Ativo,
            [ExcelArgument(Name = HelpName.cData, Description = HelpText.cData)]DateTime Data,
            [ExcelArgument(Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]int Investimento)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("COALESCE(CAR.VL_PRINCIPAL,0)", "VL_PRINCIPAL");
            oSql.AddTabela("TCLUCARTEIRA", "CAR");
            oSql.AddCondicao("AND", "CAR.DT_CARTEIRA", "=", Data);
            oSql.AddCondicao("AND", "CAR.FK_ATIVO", "=", Ativo);
            oSql.AddCondicao("AND", "USC.PK_ID", "=", Investimento);

            oSql.AddJoin("VCLUUSUCLUBE", "USC", "USC.PK_ID = CAR.FK_CLUBE");

            oSql.Executar();

            return oSql.Resultado();
        }

        [ExcelFunction(Name = "Invest.LCI.Vencimento", Category = cCategoria, Description = "Vencimento de LCI.")]
        public static object Invest_LCI_Vencimento(
                [ExcelArgument(Name = HelpName.cLCI, Description = HelpText.cLCI)]string LCI)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("LCI.DT_VENCIMENTO", "DT_VENCIMENTO");
            oSql.AddTabela("TCLUATIVOLCI", "LCI");
            oSql.AddCondicao("AND", "LCI.PK_ID", "=", LCI);

            oSql.Executar();

            return oSql.Resultado();
        }

        [ExcelFunction(Name = "Invest.Inv.Ativo.QtBloq", Category = cCategoria, Description = "Quantidade da carteira mais a quantidade bloqueada.")]
        public static object Invest_Inv_Ativo_QtBloq(
            [ExcelArgument(Name = HelpName.cAtivo, Description = HelpText.cAtivo)]string Ativo,
            [ExcelArgument(Name = HelpName.cData, Description = HelpText.cData)]DateTime Data,
            [ExcelArgument(Name = HelpName.cInvestimento, Description = HelpText.cInvestimento)]int Investimento)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("ISNULL(CAR.QT_CARTEIRA,0)+ISNULL(CAR.QT_BLOQUEADA,0)", "QT_PRAGMA");
            oSql.AddTabela("TCLUCARTEIRA", "CAR");

            oSql.AddCondicao("AND", "CAR.FK_ATIVO", "=", Ativo);
            oSql.AddCondicao("AND", "CAR.DT_CARTEIRA", "=", Data);
            oSql.AddCondicao("AND", "CAR.FK_CLUBE", "=", Investimento);

            oSql.Executar();

            return oSql.Resultado();
        }

        [ExcelFunction(Name = "Invest.Fundo.Cotacao", Category = cCategoria, Description = "Valor da cotação de um fundo.")]
        public static object Invest_Fundo_Cotacao(
            [ExcelArgument(Name = HelpName.cAtivo, Description = HelpText.cAtivo)]string Ativo,
            [ExcelArgument(Name = HelpName.cData, Description = HelpText.cData)]DateTime Data)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("ISNULL(COT.VL_COTACAO,0)", "VL_COTACAO");
            oSql.AddTabela("TCLUCOTFUNDO", "COT");

            oSql.AddCondicao("AND", "COT.FK_ATIVO", "=", Ativo);
            oSql.AddCondicao("AND", "COT.DT_COTACAO", "=", Data);

            oSql.Executar();

            return oSql.Resultado();
        }

        [ExcelFunction(Name = "Invest.Indice.Rentabilidade", Category = cCategoria, Description = "Rentabilidade do índice.")]
        public static object Invest_Indice_Rentabilidade(
                [ExcelArgument(Name = HelpName.cIndice, Description = HelpText.cIndice)]string Indice,
                [ExcelArgument(Name = HelpName.cDe, Description = HelpText.cDe)]DateTime De,
                [ExcelArgument(Name = HelpName.cAte, Description = HelpText.cAte)]DateTime Ate)
        {
            QueryBuilder oSql = new QueryBuilder();
            string cDe, cAte;

            cDe = String.Format("{0:d}", De);
            cAte = String.Format("{0:d}", Ate);

            string cComando = "SELECT dbo.SP_INDICERENTAB('" + Indice + "',CONVERT(DateTime,'" + cDe + "',103),CONVERT(DateTime,'" + cAte + "',103))";

            oSql.ExecutaSQL(cComando);

            return oSql.Resultado();
        }

        [ExcelFunction(Name = "Invest.Ativo.Cnpj", Category = cCategoria, Description = "CNPJ do Ativo (Fac Anbid).")]
        public static object Invest_Ativo_Cnpj(
            [ExcelArgument(Name = HelpName.cAtivo, Description = HelpText.cAtivo)]string Ativo)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("MAX(ISNULL(FAC.NR_CNPJ,0))", " NR_CNPJ");
            oSql.AddTabela("TCLUATIVO", "ATI");
            oSql.AddJoin("TCLUFACANBID", "FAC", "FAC.PK_ID = ATI.CD_EXTERNO");
            oSql.AddCondicao("AND", "ATI.PK_ID", "=", Ativo);

            oSql.Executar();

            return oSql.Resultado();
        }

        [ExcelFunction(Name = "Invest.Ativo.Nome", Category = cCategoria, Description = "Nome do cadastro do Ativo.")]
        public static object Invest_Ativo_Nome(
            [ExcelArgument(Name = HelpName.cAtivo, Description = HelpText.cAtivo)]string Ativo)
        {
            QueryBuilder oSql = new QueryBuilder();

            oSql.AddTop(1);
            oSql.AddCampo("ISNULL(ATI.DS_ATIVO,'')", " DS_ATIVO");
            oSql.AddTabela("TCLUATIVO", "ATI");
            oSql.AddCondicao("AND", "ATI.PK_ID", "=", Ativo);

            oSql.Executar();

            return oSql.Resultado();
        }
    }
}