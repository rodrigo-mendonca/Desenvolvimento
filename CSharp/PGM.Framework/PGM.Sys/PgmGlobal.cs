using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PGM.Sys
{
    public static class PgmGlobal
    {
        /// <summary>
        /// Nome do sistema atual
        /// </summary>
        public static string SystemName = "Pragma";

        /// <summary>
        /// Nome do sistema atual
        /// </summary>
        public static Image SystemImg;

        /// <summary>
        /// Usuario que está conectado
        /// </summary>
        public static string UserCurrent = "";
        /// <summary>
        /// Senha criptografada do usuario conectado
        /// </summary>
        public static string UserPassCurrent = "";

        /// <summary>
        /// Define um arquivo XML controlador
        /// </summary>
        public static int UserCurrentId = 0;

        /// <summary>
        /// Define um arquivo XML controlador
        /// </summary>
        public static int UserCurrentGrupId = 0;

        /// <summary>
        /// Define um arquivo XML controlador
        /// </summary>
        public static bool UserMaster = false;

        /// <summary>
        /// Nome da conexão com a base de dados
        /// </summary>
        public static string DbConnectName = "HomologConnection";

        /// <summary>
        /// Nome da conexão com a base de dados de estrutura
        /// </summary>
        public static string DbSysConnectName = "SysConnection";

        /// <summary>
        /// Define as configurações padrões do sistema
        /// </summary>
        public static void SysAmbient()
        {
            
        
        
        }

        /// <summary>
        /// Define as configurações padrões do sistema
        /// </summary>
        public static void Run(Form tForm)
        {
            tForm.Text = SystemName;

            Application.Run(tForm);
        }

        /// <summary>
        /// Retorna a Lista de conexões disponiveis
        /// </summary>
        public static List<PgmConnection> ListConnections()
        {
            List<PgmConnection> ListConnection = new List<PgmConnection>();

            #if DEBUG
            ListConnection.Add(new PgmConnection("Homologação - PGMSP01DB01", "HomologConnection"));
            ListConnection.Add(new PgmConnection("Oficial - PGMSP01DB01", "OficialConnection"));
            #else
            ListConnection.Add(new PgmConnection("Oficial - PGMSP01DB01", "OficialConnection"));
            ListConnection.Add(new PgmConnection("Homologação - PGMSP01DB01", "HomologConnection"));
            #endif

            return (ListConnection);
        }
    }
}
