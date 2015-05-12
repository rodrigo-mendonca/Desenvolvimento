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
    [Table("TCLUATIVO")]
    public class Ativo : IBase
    {
        [Key]
        [Column("PK_ID")]
        [GridHeader(Desc = "Cód")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string PkId { get; set; }

        [Column("DS_ATIVO")]
        [GridHeader(Desc = "Ativo")]
        public string Nome { get; set; }

        [Column("FK_TIPOATIVO")]
        [GridHeader(Desc = "Cód Tipo Ativo")]
        public int FkTipoAtivo { get; set; }
        [ForeignKey("FkTipoAtivo")]
        public virtual TipoAtivo TipoAtivo { get; set; }

        [Column("FK_MOEDA")]
        [GridHeader(Desc = "Cód. Moeda")]
        public decimal FkMoeda { get; set; }
        [ForeignKey("FkMoeda")]
        public virtual Indice Moeda { get; set; }


        [Column("FK_MOEDA")]
        [GridHeader(Desc = "Cód. Moeda")]
        public decimal FkMoedaLanc { get; set; }
        [ForeignKey("FkMoedaLanc")]
        public virtual Indice MoedaLanc { get; set; }

        [Column("FK_MOEDA")]
        [GridHeader(Desc = "Cód. Moeda")]
        public decimal FkMoedaOri { get; set; }
        [ForeignKey("FkMoedaOri")]
        public virtual Indice MoedaOri { get; set; }

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
        }
    }
}
