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
    [Table("TCLUPAISES")]
    public class Pais : IBase
    {
        [Key]
        [Column("PK_ID")]
        [GridHeader(Desc = "Cód")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string PkId { get; set; }

        [Column("DS_NOME")]
        [GridHeader(Desc = "País")]
        public string Nome { get; set; }

        [Column("CD_CONFAZ")]
        [GridHeader(Desc = "CONFAZ")]
        public string ConFaz { get; set; }

        [Column("CD_BACEN")]
        [GridHeader(Desc = "BACEN")]
        public string Bacen { get; set; }

        [Column("DS_NACIONALIDADE")]
        [GridHeader(Desc = "Nacionalidade")]
        public string Nacionalidade { get; set; }

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
