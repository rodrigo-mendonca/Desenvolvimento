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

        [Column("FK_MOEDAESTRANGEIRA")]
        [GridHeader(Desc = "Cód. Moeda Estrang.")]
        public string FkMoedaEstrang { get; set; }
        [ForeignKey("FkMoedaEstrang")]
        public virtual Indice MoedaEstrang { get; set; }

        [Column("FK_MOEDAATI")]
        [GridHeader(Desc = "Cód. Moeda Ativo")]
        public string FkMoeda { get; set; }
        [ForeignKey("FkMoeda")]
        public virtual Indice MoedaAti { get; set; }
        
        [Column("FK_MOEDALANC")]
        [GridHeader(Desc = "Cód. Moeda Lanc.")]
        public string FkMoedaLanc { get; set; }
        [ForeignKey("FkMoedaLanc")]
        public virtual Indice MoedaLanc { get; set; }

        [Column("FK_MOEDAORI")]
        [GridHeader(Desc = "Cód. Moeda Original")]
        public string FkMoedaOri { get; set; }
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
