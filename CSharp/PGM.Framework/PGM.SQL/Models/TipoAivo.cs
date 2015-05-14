using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PGM.Interfaces;
using PGM.Attributes;
using System.Data.Entity;

namespace PGM.SQL.Models
{
    [Table("TCLUTIPOATIVO")]
    public class TipoAtivo : IBase
    {
        [Key]
        [Column("PK_ID")]
        [GridHeader(Desc = "Cód.")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PkId { get; set; }

        [Column("DS_TIPOATIVO")]
        [GridHeader(Desc = "Tipo Ativo")]
        public string Nome { get; set; }

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
