using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.ComponentModel;

namespace SharpBook.Classes
{
    public static class Dados
    {
        public static Dictionary<String, Usuario> mUsuarios = new Dictionary<String, Usuario>();
        public static Usuario oUsuLogado = null;

        public static void PreCadastrados()
        {
            CadastraUsuario(new Usuario("James",    "123").PreConfig());
            CadastraUsuario(new Usuario("Thais", "123").PreConfig());
            CadastraUsuario(new Usuario("Julios", "123").PreConfig());
            CadastraUsuario(new Usuario("Jacques", "123").PreConfig());
            CadastraUsuario(new Usuario("Rafael", "123").PreConfig());
            CadastraUsuario(new Usuario("Clayton", "123").PreConfig());
            
        }
        public static Usuario BuscaUsu(String tcLogin)
        {
            return mUsuarios[tcLogin];
        }

        public static void CadastraUsuario(Usuario toUsu)
        {
            mUsuarios.Add(toUsu.cLogin, toUsu);
        }
        public static void CadastraUsuario(String tcLogin, String tcSenha)
        {
            mUsuarios.Add(tcLogin,new Usuario(tcLogin, tcSenha));
        }
        public static Boolean ValidaUsuario(Usuario toUsu)
        {
            Boolean lRetorno = false;

            if (mUsuarios.ContainsKey(toUsu.cLogin))
            {
                if (mUsuarios[toUsu.cLogin].cSenha == toUsu.cSenha || String.IsNullOrEmpty(toUsu.cSenha))
                {
                    lRetorno = true;
                }
            }

            return lRetorno;
        }
        public static Boolean ValidaUsuario(String tcUsu, String tcSenha)
        {
            return ValidaUsuario(new Usuario(tcUsu,tcSenha));
        }
        public static Boolean ValidaUsuario(String tcUsu)
        {
            return ValidaUsuario(new Usuario(tcUsu));;
        }

        public static void EnableControls(GroupBox toGroup, Boolean tlEnable)
        {
            foreach (Control oItem in toGroup.Controls)
            {
                oItem.Enabled = tlEnable;
            }
        }
        public static bool IsEmpty(GroupBox toGroup)
        {
            bool lRetorno = false;
            foreach (Control oItem in toGroup.Controls)
            {
                if (String.IsNullOrEmpty(oItem.Text))
                {
                    lRetorno = true;
                }
            }
            return lRetorno;
        }

        public static void EditGrid(DataGridView toGrid, List<Usuario> tuUsu)
        {
            DataGridViewImageColumn oPicColumn = new DataGridViewImageColumn();
            oPicColumn.HeaderText   = "Imagem";
            oPicColumn.Width        = 70;
            toGrid.Columns.Insert(0, oPicColumn);

            DataGridViewTextBoxColumn oTextColumn = new DataGridViewTextBoxColumn();
            oTextColumn.HeaderText = "Text";
            oTextColumn.Width = 300;
            toGrid.Columns.Insert(1, oTextColumn);

            toGrid.Rows.Clear();
            DataGridViewRow oRow;
            int nI = 0;
            foreach (Usuario uUsu in tuUsu)
            {
                oRow = new DataGridViewRow();
                if (toGrid.Rows.Count < (nI+1)){toGrid.Rows.Add(oRow);}

                toGrid.Rows[nI].SetValues(uUsu.GetImg("", 70, 70), uUsu.cNome);
                toGrid.Rows[nI].Height = 70;
                nI++;
            }
        }

        public static void EditGrid(DataGridView toGrid, List<Msg> toMsg)
        {
            DataGridViewImageColumn oPicColumn = new DataGridViewImageColumn();
            oPicColumn.HeaderText = "Imagem";
            oPicColumn.Width = 70;
            toGrid.Columns.Insert(0, oPicColumn);

            DataGridViewTextBoxColumn oTextColumn = new DataGridViewTextBoxColumn();
            oTextColumn.HeaderText = "Text";
            oTextColumn.Width = 300;
            toGrid.Columns.Insert(1, oTextColumn);

            toGrid.Rows.Clear();
            DataGridViewRow oRow;
            int nI = 0;
            foreach (Msg oMsg in toMsg)
            {
            }
        }
    }
}
