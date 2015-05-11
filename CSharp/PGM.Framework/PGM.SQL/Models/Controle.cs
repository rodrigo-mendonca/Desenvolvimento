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
    [Table("TCLUCONTROLE")]
    public class Controle : IBase
    {
        [Key]
        [Column("PK_ID")]
        [GridHeader(Desc = "Cód")]
        public int PkId { get; set; }

        [Column("DS_USUGALGO")]
        [GridHeader(Desc = "Usuário Galgo")]
        public string UsuGalgo { get; set; }

        [Column("DS_PASSGALGO")]
        [GridHeader(Desc = "Senha Galgo")]
        public string PassGalgo { get; set; }

        [Column("DS_MSGSENDERGALGO")]
        [GridHeader(Desc = "MsgSender Galgo")]
        public string MsgSenderGalgo { get; set; }

        [Column("DS_URLGALGO")]
        [GridHeader(Desc = "Url Galgo")]
        public string UrlGalgo { get; set; }

        [Column("DS_NOMEVPNGALGO")]
        [GridHeader(Desc = "Nome da VPN do Galgo")]
        public string NomeVPNGalgo { get; set; }

        [Column("DS_USUVPNGALGO")]
        [GridHeader(Desc = "Usuário da VPN do Galgo")]
        public string UsuVPNGalgo { get; set; }

        [Column("DS_PASSVPNGALGO")]
        [GridHeader(Desc = "Senha da VPN do Galgo")]
        public string PassVPNGalgo { get; set; }

        [Column("DH_ULTCONSULTAGALGO")]
        [Range(typeof(DateTime), "1/1/1900", "6/6/2079")]
        [GridHeader(Desc = "Ult. Consulta Galgo")]
        public Nullable<DateTime> LastConsultGalgo { get; set; }

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
