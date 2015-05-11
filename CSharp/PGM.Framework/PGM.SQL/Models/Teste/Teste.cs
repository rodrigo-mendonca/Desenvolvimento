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
    [Table("TCLUTESTE")]
    public class Teste : IBase
    {
        [Key]
        [Column("PK_ID")]
        [GridHeader(Desc = "Cód")]
        public int PkId { get; set; }

        [Column("DS_EXEMPLO")]
        [GridHeader(Desc = "Desc. Teste")]
        public string Desc { get; set; }

        [Column("VL_EXEMPLO")]
        [GridHeader(Desc = "Vl. Teste")]
        public decimal Valor { get; set; }

        [Column("DT_EXEMPLO")]
        [GridHeader(Desc = "Dt. Teste")]
        public Nullable<DateTime> Data { get; set; }

        [Column("TG_INATIVO")]
        public Nullable<decimal> Inativo { get; set; }

        [Column("FK_OWNER")]
        public Nullable<int> Owner { get; set; }

        [Column("DH_INCLUSAO")]
        public Nullable<DateTime> DhInclusao { get; set; }

        [Column("DH_ALTERACAO")]
        public Nullable<DateTime> DhAlteracao { get; set; }

        public virtual List<Exemplo> Exemplos { get; set; }

        public void OnCreating(DbModelBuilder oModelBuilder)
        {
        }
    }
}
