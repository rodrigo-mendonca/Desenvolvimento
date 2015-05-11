using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq.Expressions;

namespace PGM.Interfaces
{
    public interface IField<T> where T : IBase
    {
        /// <summary>
        /// Registra a propriedade a um controle
        /// </summary>
        IField<T> Register(Expression<Func<T, object>> tExpression, Control oControl);
        /// <summary>
        /// Pega o modelo adicionado ao campo
        /// </summary>
        T getModel();
        /// <summary>
        /// Adicona um modelo ao campo
        /// </summary>
        IField<T> setModel(T tModel); 
    }
}