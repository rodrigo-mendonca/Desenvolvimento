using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using conversorbinario.Classes;


namespace conversorbinario.Classes
{
    class Binario
    {
        static string log = "";

        public Binario()
        {
    
        }

        public static string toBinario(string STRING)
        {
            string BINARIO = "";
            double ascii = 0;
            
            for (int i = 0; i < STRING.Length ; i++)
			{
                ascii = Funcoes.Asc(STRING.Substring(i, 1));
                BINARIO += toBinario(ascii).ToStrzero(8);
			}
            
            return (BINARIO);
        }

        public static Strzero toBinario(double DECIMAL)
        {
            Strzero BINARIO = new Strzero();

            log = "";
            double resto = DECIMAL;

            while (resto>0)
	        {

                BINARIO.value = Convert.ToString(resto % 2) + BINARIO.value;
                log += resto.ToString() + "/2 = " + (Math.Truncate(resto / 2)).ToString() + " - " + Convert.ToString(resto % 2) + Funcoes.Chr(13);
                resto = Math.Truncate(resto / 2);
	        }

            return (BINARIO);
        }

        public static string toSTRING(string binario)
        {
            string retorno = "";
            int ascii = 0;

            for (int i = 0; i < binario.Length; i+=8)
            {
                ascii = Convert.ToInt32(toDECIMAL(binario.Substring(i,8)));
                retorno += Funcoes.Chr(ascii);
            }

            return (retorno);
        }

        public static double toDECIMAL(string binario)
        {
            double DECIMAL = 0;
            string total = "";
            string final = "";
            long bit = 0;
            int count = binario.Trim().Length;
            int x = 1;
            log = "";

            for (int i = 0; i < count; i++)
            {
                bit = Convert.ToInt64(binario.Substring(count - x, 1));
                
                DECIMAL += Math.Pow(2,i) * bit;

                if ((bit == 1) & (i != 0))
                {
                    total = (Math.Pow(2, i) * bit).ToString();
                }
                else
                {
                    total = "0";
                }
                
                log += "(2^" + i.ToString() + ") x " + bit.ToString() + " = " + total + Funcoes.Chr(13);
                final+="+"+total;

                x++;
            }

            log += "Soma " + final.Substring(1);

            return (DECIMAL);
        }

        public static string calclog()
        {
            return(log);
        }

    }
}
