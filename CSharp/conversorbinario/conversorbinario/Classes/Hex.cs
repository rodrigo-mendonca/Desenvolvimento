using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace conversorbinario.Classes
{
    class Hex
    {
        static string log = "";

        public Hex()
        {
    
        }

        public static string toHex(double DECIMAL)
        {
            string retorno = "";
            double sobra = 0;
            int hex = 0;

            sobra = Math.Truncate(DECIMAL / 16);

            hex = Convert.ToInt32(DECIMAL - (sobra * 16));
            
            retorno = tabela(hex) + retorno;

            while (sobra>=16)
            {
                DECIMAL = sobra;
                sobra = Math.Truncate(sobra / 16);
                hex = Convert.ToInt32(DECIMAL - (sobra * 16));
                retorno = tabela(hex) + retorno;
            }
            retorno = tabela(Convert.ToInt32(sobra)) + retorno;

            return(retorno);
        }

        public static string calclog()
        {
            return(log);
        }

        private static string tabela(int numero)
        {
            string retorno = "";

            if (numero<10)
            {
                retorno = numero.ToString();

                return (retorno);
            }

            switch (numero)
            {
                case 10:
                    retorno = "A";
                    break;
                case 11:
                    retorno = "B";
                    break;
                case 12:
                    retorno = "C";
                    break;
                case 13:
                    retorno = "D";
                    break;
                case 14:
                    retorno = "E";
                    break;
                case 15:
                    retorno = "F";
                   break;
                default:
                    break;
            }

            return (retorno);
        }

    }
}
