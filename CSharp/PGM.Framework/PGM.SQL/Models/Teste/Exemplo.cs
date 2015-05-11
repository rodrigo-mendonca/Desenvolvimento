using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PGM.Interfaces;
using PGM.Attributes;
using System.Data.Entity;
using System.Xml.Serialization;

namespace PGM.SQL.Models
{
    [Table("TCLUEXEMPLO")]
    public class Exemplo : IBase
    {
        public Exemplo()
        {
            Owner = 48;
        }

        [Key]
        [Column("PK_ID")]
        [GridHeader(Desc = "Cód")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string PkId { get; set; }

        [Column("DS_EXEMPLO")]
        [GridHeader(Desc = "Desc. Exemplo")]
        public string Desc { get; set; }

        [Column("VL_EXEMPLO")]
        [GridHeader(Desc = "Vl. Exemplo")]
        public decimal Valor { get; set; }

        [Column("FK_TESTE")]
        [GridHeader(Desc = "Cód Teste")]
        public int FkTeste { get; set; }
        [ForeignKey("FkTeste")]
        [XmlIgnoreAttribute]
        public virtual Teste Teste { get; set; }

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
