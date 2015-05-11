using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PGM.Controls;
using PGM.WebService.Galgo;

namespace PGM.Sistema
{
    public class PgmContextMenu : ContextMenu
    {
        public PgmContextMenu()
        {
    
        }

        /// <summary>
        /// Adiciona um novo menu de contexto
        /// </summary>
        /// <param name="tName">Nome do Menu</param>
        /// <param name="tEvent">Evento que será executado ao clicar no menu</param>
        public void AddMenu(string tName, EventHandler tEvent)
        {
            MenuItem oItem = new MenuItem();
            oItem.Click += tEvent;
            oItem.Text = tName;
            MenuItems.Add(oItem);
        }

        /// <summary>
        /// Adiciona um novo menu de contexto
        /// </summary>
        /// <param name="tName">Nome do Menu</param>
        /// <param name="tForm">Nome do Formulario com o namespace</param>
        public void AddMenu(string tName, string tForm)
        {
            AddMenu(tName, tForm,null,null);
        }

        /// <summary>
        /// Adiciona um novo menu de contexto
        /// </summary>
        /// <param name="tName">Nome do Menu</param>
        /// <param name="tForm">Nome do Formulario com o namespace</param>
        /// <param name="tParams">Lista de parametros</param>
        public void AddMenu(string tName, string tForm,params object[] tParams)
        {
            AddMenu(tName, tForm, null, tParams);
        }

        /// <summary>
        /// Adiciona um novo menu de contexto
        /// </summary>
        /// <param name="tName">Nome do Menu</param>
        /// <param name="tForm">Nome do Formulario com o namespace</param>
        /// <param name="tBase">Form que está fazendo a chamada,usado para forms modal</param>
        /// <param name="tParams">Lista de parametros</param>
        public void AddMenu(string tName, string tForm, FormBase tBase,params object[] tParams)
        {
            FormBase oForm = (FormBase)System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(tForm);

            if (tParams != null)
            {
                oForm.ListParams = tParams;
                oForm.oFormParent = tBase;
            }
            if (tBase==null)
                AddMenu(tName, (_, __) => { oForm.Show(); });
            else
                AddMenu(tName, (_, __) => { oForm.ShowDialog(tBase); });
        }
    }
}
