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
    [Table("TCLUFERIADO")]
    public class Feriado : IBase
    {
        [Key]
        [Column("PK_ID")]
        [GridHeader(Desc = "Cód.")]
        public int PkId { get; set; }

        [Column("DT_FERIADO")]
        [GridHeader(Desc = "Dt. Feriado")]
        public DateTime DtFeriado { get; set; }

        [Column("DS_FERIADO")]
        [GridHeader(Desc = "Feriado")]
        public string Nome { get; set; }

        [Column("TG_PERIODO")]
        [GridHeader(Desc = "Fixo")]
        public int Fixo { get; set; }

        [Column("FK_PAIS")]
        [GridHeader(Desc = "Cód. Pais")]
        public string FkPais { get; set; }
        [ForeignKey("FkPais")]
        public virtual Pais Pais { get; set; }

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
