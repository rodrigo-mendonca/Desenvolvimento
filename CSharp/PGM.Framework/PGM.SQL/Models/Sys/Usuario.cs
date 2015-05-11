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
    [Table("TCLUUSUARIO")]
    public class Usuario : IBase
    {
        [Key]
        [Column("PK_ID")]
        [GridHeader(Desc = "Cód")]
        public int PkId { get; set; }

        [Column("DS_LOGIN")]
        [GridHeader(Desc = "Login")]
        public string Login { get; set; }

        [Column("DS_OBS")]
        [GridHeader(Desc = "Obs.")]
        public string Obs { get; set; }

        [Column("DS_SENHA")]
        [GridHeader(Desc = "Senha")]
        public string Senha { get; set; }

        [Column("FK_GRUPO")]
        [GridHeader(Desc = "Grupo")]
        public int Grupo { get; set; }

        [Column("DS_EMAIL")]
        [GridHeader(Desc = "Email")]
        public string Email { get; set; }

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
