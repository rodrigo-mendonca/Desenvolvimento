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
    [Table("TSISAJUDASISTEMA")]
    public class AjudaSistema : IBase
    {
        [Key]
        [Column("PK_ID")]
        [GridHeader(Desc = "Cód")]
        public int PkId { get; set; }

        [Column("DS_NAME")]
        [GridHeader(Desc = "Nome")]
        public string Name { get; set; }

        [Column("DS_CAMPO")]
        [GridHeader(Desc = "Campo")]
        public string Campo { get; set; }

        [Column("DS_FORM")]
        [GridHeader(Desc = "Form")]
        public string Form { get; set; }

        [Column("DS_PARENT")]
        [GridHeader(Desc = "Parent")]
        public string Parent { get; set; }

        [Column("NR_ORDEM")]
        [GridHeader(Desc = "Ordem")]
        public int Ordem { get; set; }

        [Column("DS_AJUDA")]
        [GridHeader(Desc = "Ajuda")]
        public string Ajuda { get; set; }

        [Column("TG_SISTEMA")]
        [GridHeader(Desc = "Sistema")]
        public string Sistema { get; set; }

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
            Sistema = "Net";
        }
    }
}
