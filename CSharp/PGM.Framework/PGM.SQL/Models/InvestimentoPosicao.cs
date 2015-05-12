using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PGM.Interfaces;
using PGM.Attributes;
using System.Data.Entity;

namespace PGM.SQL.Models
{
    [Table("TCLUCARTEIRA")]
    public class InvestimentoPosicao : IBase
    {
        [Key]
        [Column("PK_ID")]
        [GridHeader(Desc = "Cód.")]
        public decimal PkId { get; set; }

        [Column("FK_CLUBE")]
        [GridHeader(Desc = "Cód Investimento")]
        public decimal FkInvestimento { get; set; }
        [ForeignKey("FkInvestimento")]
        public virtual Investimento Investimento { get; set; }

        [Column("FK_ATIVO")]
        [GridHeader(Desc = "Cód. Ativo")]
        public string FkAtivo { get; set; }
        [ForeignKey("FkAtivo")]
        public virtual Ativo Ativo { get; set; }

        [Column("DS_VENCIMENTO")]
        [GridHeader(Desc = "Vencimento/Cód")]
        public string Vencimento { get; set; }

        [Column("DT_CARTEIRA")]
        [GridHeader(Desc = "Dt. Posição")]
        public DateTime DtPosicao { get; set; }

        [Column("QT_CARTEIRA")]
        [GridHeader(Desc = "Qt. Carteira")]
        public decimal QtCarteira { get; set; }

        [Column("QT_BLOQUEADA")]
        [GridHeader(Desc = "Qt. Bloqueada")]
        public decimal QtBloqueada { get; set; }

        [Column("QT_PENDENTE")]
        [GridHeader(Desc = "Qt. Pendente")]
        public decimal QtPendente { get; set; }

        [Column("VL_COTACAO")]
        [GridHeader(Desc = "Vl. Cotação")]
        public decimal VlCotacao { get; set; }

        [Column("VL_CUSMED")]
        [GridHeader(Desc = "Vl. Custo Médio")]
        public decimal VlCusMed { get; set; }

        [Column("VL_PRETOT")]
        [GridHeader(Desc = "Vl. PreTot")]
        public decimal VlPreTot { get; set; }

        [Column("VL_EMTRANSITO")]
        [GridHeader(Desc = "Vl. EmTransito")]
        public decimal VlEmTransito { get; set; }

        [Column("VL_BRUTO")]
        [GridHeader(Desc = "Vl. Bruto")]
        public decimal VlBruto { get; set; }

        [Column("VL_IRRF")]
        [GridHeader(Desc = "Vl. Irrf")]
        public decimal VlIrrf { get; set; }

        [Column("VL_PATRIMONIO")]
        [GridHeader(Desc = "Vl. Patrimonio")]
        public decimal VlPatrimonio { get; set; }

        [Column("VL_VENCTO")]
        [GridHeader(Desc = "Vl. VencTo")]
        public decimal VlVencTo { get; set; }

        [Column("VL_PORVARDIA")]
        [GridHeader(Desc = "Vl. PorVarDia")]
        public decimal VlPorVarDia { get; set; }

        [Column("VL_AJUSTE1")]
        [GridHeader(Desc = "Vl. Dividendo")]
        public decimal VlDividendo { get; set; }

        [Column("DT_INICIO")]
        [GridHeader(Desc = "Dt. Inicio")]
        public DateTime DtInicio { get; set; }

        [Column("QT_LOTE")]
        [GridHeader(Desc = "Qt. Lote")]
        public decimal QtLote { get; set; }

        [Column("FK_TIPOATIVO")]
        [GridHeader(Desc = "Cód. Tipo Ativo")]
        public int FkTipoAtivo { get; set; }
        [ForeignKey("FkTipoAtivo")]
        public virtual TipoAtivo TipoAtivo { get; set; }

        [Column("CD_OPCAO")]
        [GridHeader(Desc = "Cód. Opção")]
        public string CdOpcao { get; set; }

        [Column("VL_CUSTOT")]
        [GridHeader(Desc = "Vl. Custo Total")]
        public decimal VlCusTot { get; set; }

        [Column("VL_MOEDA")]
        [GridHeader(Desc = "Vl. Moeda")]
        public decimal VlMoeda { get; set; }

        [Column("VL_PATRIMONIOANT")]
        [GridHeader(Desc = "Vl. Patrimonio Anterior")]
        public decimal VlPatrimonioAnt { get; set; }

        [Column("VL_MOVIMENTOANT")]
        [GridHeader(Desc = "Vl. Movimento")]
        public decimal VlMovimentoAnt { get; set; }

        [Column("FK_PARENT")]
        [GridHeader(Desc = "Parent")]
        public decimal FkParent { get; set; }

        [Column("TG_ESTRANGEIRO")]
        [GridHeader(Desc = "Estrangeiro")]
        public decimal TgEstrangeiro { get; set; }

        [Column("TG_SEMCOTACAO")]
        [GridHeader(Desc = "Sem Cotação")]
        public decimal TgSemCotacao { get; set; }

        [Column("VL_PORTAXA")]
        [GridHeader(Desc = "Vl. Por. Taxa")]
        public decimal VlPorTaxa { get; set; }

        [Column("FK_MOEDA")]
        [GridHeader(Desc = "Cód. Moeda")]
        public decimal FkMoeda { get; set; }
        [ForeignKey("FkMoeda")]
        public virtual Indice Moeda { get; set; }

        [Column("TG_SEMQTD")]
        [GridHeader(Desc = "Sem QTD.")]
        public int TgSemQtd { get; set; }

        [Column("VL_OFFMOV")]
        [GridHeader(Desc = "Vl. Off. Mov.")]
        public decimal VlOffMov { get; set; }

        [Column("VL_OFFREN")]
        [GridHeader(Desc = "Vl. Off. Ren.")]
        public decimal VlOffRen { get; set; }

        [Column("VL_COTREAL")]
        [GridHeader(Desc = "Vl. Cot. Real")]
        public decimal VlCotReal { get; set; }

        [Column("VL_COTREAL")]
        [GridHeader(Desc = "Vl. Cot. Real")]
        public decimal VlCOTREAL { get; set; }

        [Column("TG_DESCONSIDERAR")]
        [GridHeader(Desc = "Desconsiderar")]
        public int TgDesconsiderar { get; set; }

        [Column("VL_PRINCIPAL")]
        [GridHeader(Desc = "Vl. Principal")]
        public decimal VlPrincipal { get; set; }

        [Column("VL_MOVPRINCIPAL")]
        [GridHeader(Desc = "Vl. Mov. Principal")]
        public decimal VlMovPrincipal { get; set; }

        [Column("VL_COTARENT")]
        [GridHeader(Desc = "Vl. Rentab. Cota")]
        public decimal VlCotaRent { get; set; }

        [Column("CD_INTERNOGESTOR")]
        [GridHeader(Desc = "Cód Interno Gestor")]
        public string CdInternoGestor { get; set; }

        [Column("VL_IOF")]
        [GridHeader(Desc = "Vl. IOF")]
        public decimal VlIof { get; set; }

        [Column("VL_PATRBRUTO")]
        [GridHeader(Desc = "Vl. Patr. Bruto")]
        public decimal VlPatrBruto { get; set; }

        [Column("VL_MOVTOAJUSTEBRUTO")]
        [GridHeader(Desc = "Vl. Mov. Ajuste Bruto")]
        public decimal VlMovtoAjusteBruto { get; set; }

        [Column("NR_CNPJINTER")]
        [GridHeader(Desc = "CNPJ Inter.")]
        public decimal NrCNPJInter { get; set; }

        [Column("VL_TXALUG")]
        [GridHeader(Desc = "Vl. taxa de Aluguel")]
        public decimal VlTxAlug { get; set; }

        [Column("TG_CLASSEOPERACAO")]
        [GridHeader(Desc = "Classe Opção")]
        public char TgClasseOperacao { get; set; }

        [Column("TG_MANTERIRRF")]
        [GridHeader(Desc = "Manter IRRF")]
        public decimal TgManterIrrf { get; set; }

        [Column("TG_MANTERMOVTOANT")]
        [GridHeader(Desc = "Manter Mov.Ant.")]
        public decimal TgManterMovtoAnt { get; set; }

        [Column("VL_MOVTOBRUTO")]
        [GridHeader(Desc = "Vl. Mov. Bruto")]
        public decimal TgMovtoBruto { get; set; }

        [Column("VL_PATRIMONIOBANCO")]
        [GridHeader(Desc = "Vl. Patrimonio do Banco")]
        public decimal VlPatrimonioBanco { get; set; }

        [Column("VL_MOVBANCO")]
        [GridHeader(Desc = "Vl. Mov. do Banco")]
        public decimal VlMovBanco { get; set; }

        [Column("DT_VENCIMENTO")]
        [GridHeader(Desc = "Dt. Vencimento")]
        public Nullable<DateTime> DtVencimento { get; set; }

        [Column("VL_TXGES")]
        [GridHeader(Desc = "Vl. Tx. Gestão")]
        public Nullable<decimal> VlTxGes { get; set; }

        [Column("VL_TXADM")]
        [GridHeader(Desc = "Vl. Tx. Administração")]
        public Nullable<decimal> VlTxAdm { get; set; }

        [Column("TG_INATIVO")]
        public Nullable<decimal> Inativo { get; set; }

        [Column("FK_OWNER")]
        public Nullable<int> Owner { get; set; }

        [Column("DH_INCLUSAO")]
        public Nullable<DateTime> DhInclusao { get; set; }

        [Column("DH_ALTERACAO")]
        public Nullable<DateTime> DhAlteracao { get; set; }

        public void OnCreating(DbModelBuilder oModelBuilder)
        {
            oModelBuilder.Entity<InvestimentoPosicao>().Property(i => i.QtCarteira).HasPrecision(20, 8);
            oModelBuilder.Entity<InvestimentoPosicao>().Property(i => i.VlCotacao).HasPrecision(18, 8);
            
        }
    }
}
