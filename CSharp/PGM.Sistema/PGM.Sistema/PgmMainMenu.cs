using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PGM.Controls;
using System.Reflection;

namespace PGM.Sistema 
{
    public class PgmMenuItem : MenuItem
    {
        public PgmMainMenu oMainMenu;

        /// <summary>
        /// Adiciona um novo menu de contexto
        /// </summary>
        /// <param name="tName">Nome do Menu</param>
        public PgmMenuItem AddMenuSub(string tName)
        {
            PgmMenuItem oItem = new PgmMenuItem();
            oItem.oMainMenu = oMainMenu;
            oItem.Text = tName;
            this.MenuItems.Add(oItem);
            return oItem;
        }


        /// <summary>
        /// Adiciona um novo menu de contexto
        /// </summary>
        /// <param name="tName">Nome do Menu</param>
        /// <param name="tEvent">Evento que será executado ao clicar no menu</param>
        private void AddMenu(string tName, EventHandler tEvent)
        {
            PgmMenuItem oItem = new PgmMenuItem();
            oItem.oMainMenu = oMainMenu;
            oItem.Click += tEvent;
            oItem.Text = tName;
            MenuItems.Add(oItem);
        }

        /// <summary>
        /// Adiciona um novo menu de contexto
        /// </summary>
        /// <param name="tName">Nome do Menu</param>
        /// <param name="tForm">Nome do Formulario com o namespace</param>
        /// <param name="tBase">Form que está fazendo a chamada,usado para forms modal</param>
        /// <param name="tParams">Lista de parametros</param>
        public void AddMenu(string tName, Form tForm, Form tBase)
        {
            oMainMenu.oBase = tBase;
            AddMenu(tName, new EventHandler((sender, eventArgs) => oMainMenu.Callform(tForm, eventArgs)));
        }
    }

    public class PgmMainMenu : MainMenu
    {
        public Form oBase;
        public PgmMainMenu()
        {
    
        }

        /// <summary>
        /// Adiciona um novo menu de contexto
        /// </summary>
        /// <param name="tName">Nome do Menu</param>
        public PgmMenuItem AddMenuSub(string tName)
        {
            PgmMenuItem oItem = new PgmMenuItem();
            oItem.oMainMenu = this;
            oItem.Text = tName;
            MenuItems.Add(oItem);
            return oItem;
        }

        /// <summary>
        /// Adiciona um novo menu de contexto
        /// </summary>
        /// <param name="tName">Nome do Menu</param>
        /// <param name="tEvent">Evento que será executado ao clicar no menu</param>
        private void AddMenu(string tName, EventHandler tEvent)
        {
            PgmMenuItem oItem = new PgmMenuItem();
            oItem.oMainMenu = this;
            oItem.Click += tEvent;
            oItem.Text = tName;
            MenuItems.Add(oItem);
        }

        /// <summary>
        /// Adiciona um novo menu de contexto
        /// </summary>
        /// <param name="tName">Nome do Menu</param>
        /// <param name="tForm">Nome do Formulario com o namespace</param>
        /// <param name="tBase">Form que está fazendo a chamada,usado para forms modal</param>
        /// <param name="tParams">Lista de parametros</param>
        public void AddMenu(string tName, Form tForm, Form tBase)
        {
            oBase = tBase;
            AddMenu(tName, new EventHandler((sender, eventArgs) => Callform(tForm, eventArgs)));
        }

        public void Callform(object o, EventArgs e)
        {
            Type t = o.GetType();
            FormBase oForm = (FormBase)Activator.CreateInstance(t);
            oForm.MdiParent = oBase;
            oForm.Show();
        }
    }
}
