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
    [Table("TCLUGALGOLOG")]
    public class GalgoLog : IBase
    {
        [Key]
        [Column("PK_ID")]
        [GridHeader(Desc = "Cód")]
        public int PkId { get; set; }

        [Column("DT_INICIO")]
        [GridHeader(Desc = "Dt. Inicio")]
        public Nullable<DateTime> DtInicio { get; set; }

        [Column("DT_FINAL")]
        [GridHeader(Desc = "Dt. Final")]
        public Nullable<DateTime> DtFinal { get; set; }

        [Column("TG_ACAO")]
        [GridHeader(Desc = "Ação")]
        public string Acao { get; set; }

        [Column("NR_REG")]
        [GridHeader(Desc = "Registros")]
        public int NrReg { get; set; }

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
            oModelBuilder.Entity<Galgo>().Property(i => i.VlCota).HasPrecision(20, 12);
            oModelBuilder.Entity<Galgo>().Property(i => i.VlPatrimonio).HasPrecision(20, 5);
        }
    }
}
