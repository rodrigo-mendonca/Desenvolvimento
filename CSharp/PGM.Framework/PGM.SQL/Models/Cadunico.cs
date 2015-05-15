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
    [Table("TCLUCADUNICO_NEW")]
    public class Cadunico : IBase
    {
        [Key]
        [Column("PK_ID")]
        [GridHeader(Desc = "Cód.")]
        public int PkId { get; set; }

        [Column("TG_PESSOA")]
        [GridHeader(Desc = "Pessoa")]
        public string TgPessoa { get; set; }

        [Column("TG_TIPOFUNDO")]
        [GridHeader(Desc = "Tipo de Fundo")]
        public char TgTipofundo { get; set; }

        [Column("FK_TIPO")]
        [GridHeader(Desc = "Cód. Tipo")]
        public int FkTipo { get; set; }

        [Column("NR_CNPJ")]
        [GridHeader(Desc = "Nr. CNPJ")]
        public decimal NrCnpj { get; set; }

        [Column("DS_FANTASIA")]
        [GridHeader(Desc = "Fantasia")]
        public string Fantasia { get; set; }

        [Column("DS_RAZAO")]
        [GridHeader(Desc = "Razão")]
        public string Razao { get; set; }

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