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
    [Table("TCLUCLUBE")]
    public class Investimento : IBase
    {
        [Key]
        [Column("PK_ID")]
        [GridHeader(Desc = "Cód.")]
        public decimal PkId { get; set; }

        [Column("DS_FANTASIA")]
        [GridHeader(Desc = "Investimento")]
        public string Fantasia { get; set; }

        [Column("DS_CLUBE")]
        [GridHeader(Desc = "Nome do Investimento")]
        public string Nome { get; set; }

        [Column("FK_ATIVO")]
        [GridHeader(Desc = "Cód. Ativo")]
        public string FkAtivo { get; set; }
        [ForeignKey("FkAtivo")]
        public virtual Ativo Ativo { get; set; }

        [Column("FK_MOEDA")]
        [GridHeader(Desc = "Cód. Moeda")]
        public decimal FkMoeda { get; set; }
        [ForeignKey("FkMoeda")]
        public virtual Indice Moeda { get; set; }

        [Column("TG_INATIVO")]
        public Nullable<decimal> Inativo { get; set; }

        [Column("FK_OWNER")]
        [GridHeader(Desc = "Usuario")]
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