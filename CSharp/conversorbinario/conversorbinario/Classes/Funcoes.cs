using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace conversorbinario.Classes
{
    class Funcoes
    {
        public static string Extract(string text, string n1, string n2, int oco)
        {
            string retorno = "";

            int oco1 = 0;

            int oco2 = text.Length;

            if (!string.IsNullOrEmpty(n1))
            {
                oco1 = text.IndexOf(n1) + n1.Length;
            }

            if (!string.IsNullOrEmpty(n2))
            {
                oco2 = text.IndexOf(n2);
            }
            
            for (int i = 1; i < oco; i++)
            {
                oco1 = text.IndexOf(n1, oco1) + n1.Length;
                oco2 = text.IndexOf(n2, oco1);
            }

            if (oco1 == 0) { return (retorno); }
            if (oco2 == 0) { return (retorno); }

            retorno = text.Substring(oco1, oco2 - oco1);

            return (retorno);
        }

        public static char Chr(int codigo)
        {
            return (char)codigo;
        }

        public static int Asc(string letra)
        {
            
            return(int)(Convert.ToChar(letra));        
        }
        
    }
}
