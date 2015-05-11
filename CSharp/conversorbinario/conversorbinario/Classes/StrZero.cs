using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace conversorbinario.Classes
{
    class Strzero
    {
        public string value;

        public string ToStrzero(int tamanho)
        {
            string retorno = value;
            if (value.Length >= tamanho)
            {
                return (retorno);
            }
            else
            {
                for (int i = value.Length; i < tamanho; i++)
                {
                    value = "0" + value;
                }
                retorno = value;

                return (retorno);
            }
        }

        public string Value()
        {
            return (value);
        }
    }
}
