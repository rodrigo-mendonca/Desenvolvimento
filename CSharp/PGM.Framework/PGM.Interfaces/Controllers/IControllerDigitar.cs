using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace PGM.Interfaces
{
    public interface IControllerDigitar
    {
        /// <summary>
        /// Texto da label de apresentação
        /// </summary>
        string cLabel { get; set; }
        /// <summary>
        /// Define o Id que o controlador está trabalhando
        /// </summary>
        void SetId(object tId);
        /// <summary>
        /// Salva o modelo no bando de dados
        /// </summary>
        bool SaveChances();
        /// <summary>
        /// Define qual é o conexão para o repositorio
        /// </summary>
        void SetConnection(string tNameOrConnectionString);
    }

    public interface IControllerDigitar<T> : IControllerDigitar where T : IBase
    {
        /// <summary>
        /// Registra a propriedade do modelo para um controle
        /// </summary>
        IControllerDigitar<T> Register(Expression<Func<T, object>> tExpression, Control oControl);
        /// <summary>
        /// Adicionar um modelo para o controlador
        /// </summary>
        IField<T> SetEE(T tModel);
        /// <summary>
        /// Pega o modelo que está no controlador
        /// </summary>
        T GetEE();
    }
}
