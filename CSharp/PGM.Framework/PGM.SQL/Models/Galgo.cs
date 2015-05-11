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
    [Table("TCLUGALGOIMP")]
    public class Galgo : IBase
    {
        [Key]
        [Column("PK_ID")]
        [GridHeader(Desc = "Cód")]
        public int PkId { get; set; }

        [Column("DT_IMPORTACAO")]
        [GridHeader(Desc = "Dt. Importação")]
        public Nullable<DateTime> DtImportacao { get; set; }

        [Column("DS_FUNDO")]
        [GridHeader(Desc = "Nome do Fundo")]
        public string NmFundo { get; set; }

        [Column("DT_CONSULTA")]
        [GridHeader(Desc = "Dt. Consulta")]
        public Nullable<DateTime> DtConsulta { get; set; }

        [Column("DT_FUNDO")]
        [GridHeader(Desc = "Dt. Fundo")]
        public Nullable<DateTime> DtFundo { get; set; }

        [Column("CD_STIFUNDO")]
        [GridHeader(Desc = "Cód Sti. do Fundo")]
        public int StiFundo { get; set; }

        [Column("DS_FUNDORAZAO")]
        [GridHeader(Desc = "Razão do Fundo")]
        public string FundoRazao { get; set; }

        [Column("DS_NMADMINISTRATOR")]
        [GridHeader(Desc = "Administrator")]
        public string NmAdministrator { get; set; }

        [Column("VL_COTA")]
        [GridHeader(Desc = "Vl. Cota")]
        public decimal VlCota { get; set; }

        [Column("CD_VALIDACAO")]
        [GridHeader(Desc = "Cód. Validação")]
        public string CodValidacao { get; set; }

        [Column("TG_OFICIAL")]
        [GridHeader(Desc = "Oficial")]
        public decimal Oficial { get; set; }

        [Column("TG_CLASSESUSP")]
        [GridHeader(Desc = "Classes USP")]
        public int ClassesUSP { get; set; }

        [Column("DS_OBS")]
        [GridHeader(Desc = "Observação")]
        public string Observacao { get; set; }

        [Column("VL_PATRIMONIO")]
        [GridHeader(Desc = "Vl. Patrimonio")]
        public decimal VlPatrimonio { get; set; }

        [Column("TG_INATIVO") ]
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
