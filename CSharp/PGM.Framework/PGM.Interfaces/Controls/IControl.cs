using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGM.Interfaces
{
    public interface IControl
    {
        /// <summary>
        /// Inclui o valor na editbox
        /// </summary>
        /// <param name="tText">Valor para a Text</param>
        void SetValue(object tText);

        /// <summary>
        /// Altera a cor da editbox para a cor padrão de obrigatorio
        /// </summary>
        /// <returns>Retorna o valor que recebeu</returns>
        bool SetObrigatory(bool lOb);
        /// <summary>
        /// Pegar o valor da editbox
        /// </summary>
        /// <returns>Retorna o valor que está na editbox</returns>
        object GetValue();

        /// <summary>
        /// Verifica se o campo é obrigatorio
        /// </summary>
        /// <returns>true ou false para indicar se é obrigatorio</returns>
        bool IsObrigatory();

        /// <summary>
        /// Verifica se o campo está vazio
        /// </summary>
        /// <returns>true ou false para indicar se está vazio</returns>
        bool IsEmpty();
    }
}
