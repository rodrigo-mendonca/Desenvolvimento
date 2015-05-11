using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InvestExcel.Controle
{
    public static class HelpText
    {
        public const string cAtivo          = "Esse campo tem que ser entre aspas";
        public const string cInvestimento   = "Um ou mais códigos (cada investimento deve ser passado separado para agrupar)";
        public const string cData           = "Esse campo tem que ser entre aspas";
        public const string cDe             = "Esse campo tem que ser entre aspas";
        public const string cAte            = "Esse campo tem que ser entre aspas";
        public const string cLCI            = "Esse campo tem que ser entre aspas";
        public const string cClasse         = "Campo númerico";
        public const string cMoeda          = "Esse campo tem que ser entre aspas";
        public const string cNivel          = "Niveis possiveis(CONSOLIDADO,LOCAL,DESCONSIDERAR,DESCONSIDERAR,SETOR,SETORCONS,CLASSE,CONJUNTO,ATIVO";
        public const string cIndice         = "Esse campo tem que ser entre aspas";
        public const string cTipoAtivo      = "Campo númerico";
        public const string cCotista        = "Campo númerico";
        public const string cTaxa           = "Campo númerico";
        public const string cLocal          = "0-Local ou 1-Internacional ou 2-Consolidado";
        public const string cVencimento     = "Esse campo tem que ser entre aspas";
        public const string cOpcao          = "Esse campo tem que ser entre aspas";
        public const string cInternoGestor  = "Esse campo tem que ser entre aspas";
        public const string cSubClasse      = "Campo númerico";
        public const string cDesConsiderar  = "0-Ativos Considerados ou 1-Ativos não considerados";
    }

    public static class HelpName
    {
        public const string cAtivo          = "Código do Ativo";
        public const string cInvestimento   = "Código do Investimento";
        public const string cData           = "Data da posição";
        public const string cDe             = "Data inicial da consulta";
        public const string cAte            = "Data final da consulta";
        public const string cLCI            = "Código da LCI no Invest";
        public const string cClasse         = "Codigo da Classe";
        public const string cMoeda          = "Código da Moeda";
        public const string cNivel          = "Nome do Nivel";
        public const string cIndice         = "Código do Indice";
        public const string cTipoAtivo      = "Código de tipo ativo";
        public const string cCotista        = "Código do Cotista";
        public const string cTaxa           = "Código da Taxa";
        public const string cLocal          = "Local ou Internacional";
        public const string cVencimento     = "Vencimento do ativo ou código da LCI/LCA";
        public const string cOpcao          = "Código da Opção";
        public const string cInternoGestor  = "Código interno do gestor";
        public const string cSubClasse      = "Codigo da Sub.Classe";
        public const string cDesConsiderar  = "Considerado ou Não considerado";
    }
}
