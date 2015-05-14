using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PGM.Sys;
using PGM.Modules;
using PGM.Test.SQL;

namespace PGM.Test
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            PgmModulo oMod = new PgmModulo();
            oMod.SetBinding(PgmInjector.SjContainer);

            // verifica se todos os modelos estão funcionando ou seja estão mapeados corretamente
            SQLModelsTest.StartTest();
        }
    }
}
