using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BaseConvert
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmBaseConvert());
        }
    }

    public class Conversor
    {
        public string cIn   = "";
        public string cOut  = "";
        public bool lInvert = false;
        public string cBaseIn = "";
        public string cBaseOut = "";

        public Conversor(string tcIn)
        { 
            cIn = tcIn;
        }

        public string[] GetTipos()
        {
            string[] cRetorno = new string[5];

            cRetorno[0] = "String";
            cRetorno[1] = "Number";
            cRetorno[2] = "Binary";
            cRetorno[3] = "Hex";
            cRetorno[4] = "Base64";

            return (cRetorno);
        }

        public void Converter()
        {
            if (cBaseIn == "" || cBaseOut == "") { return; }

            cOut = "";
            if (cIn == "") { return; }

            Bases bBase = new Bases(cBaseIn, cIn ,cBaseOut);
            cOut = bBase.GetResult();
        }
    }
}
