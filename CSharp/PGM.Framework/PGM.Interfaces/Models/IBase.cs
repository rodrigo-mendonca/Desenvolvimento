using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PGM.Attributes;

namespace PGM.Interfaces
{
    public interface IBase
    {
        /// <summary>
        /// Propriedade indicando se o registro está ativo
        /// </summary>
        [GridHeader(Desc = "Inativo")]
        Nullable<decimal> Inativo { get; set; }
        /// <summary>
        /// Propriedade indicando qual é o código do usuario que incluiu o registro
        /// </summary>
        [GridHeader(Desc = "Usuario")]
        Nullable<int> Owner { get; set; }
        /// <summary>
        /// Propriedade contendo a data de criação do registro
        /// </summary>
        [GridHeader(Desc = "Data e Hora de Inclusão")]
        [Range(typeof(DateTime), "1/1/1900", "6/6/2079")]
        Nullable<DateTime> DhInclusao { get; set; }
        /// <summary>
        /// Propriedade contendo a data de atualização do registro
        /// </summary>
        [GridHeader(Desc = "Data e Hora da Alteração")]
        [Range(typeof(DateTime), "1/1/1900", "6/6/2079")]
        Nullable<DateTime> DhAlteracao { get; set; }
        /// <summary>
        /// Metodo que executa qual criar o modelo
        /// </summary>
        void OnCreating(DbModelBuilder oModelBuilder);

    }
}
