using System;
using System.Collections.Generic;
using System.Text;

namespace BaseConvert
{
    class Bases
    {
        private string cFrom = "";
        private string cTo = "";

        private string cResult = "";

        public Bases(string tcFrom, string tcText, string tcTo)
        {
            cFrom = tcFrom;
            cTo = tcTo;
            cResult = tcText;

            ToBase();
        }

        public string GetResult()
        {
            return(cResult);
        }

        private void ToBase()
        {
            switch (cFrom.ToUpper())
            {
                case "STRING":
                    cResult = ToString(cResult);
                    break;
                case "NUMBER":

                    break;
                case "BINARY":

                    break;
                case "HEX":

                    break;
                case "BASE64":

                    break;
                default:
                    break;
            }
        
        }

        private string ToString(string tcText)
        {
            string cRetorno = "";

            switch (cTo)
            {
                //case "NUMBER":
                //    if (Convert.ToDouble(tcText) > 255)
                //    {
                //        return(tcText);
                //    }

                //    cResult = 

                //    break;
                default:
                    break;
            }

            return (cRetorno);
        }

        private string ToBinary(string tcText)
        {
            string cRetorno = "";



            return (cRetorno);
        }

        private string ToNumber(string tcText)
        {
            string cRetorno = "";



            return (cRetorno);
        }

        private string ToHex(string tcText)
        {
            string cRetorno = "";



            return (cRetorno);
        }
    }
}
