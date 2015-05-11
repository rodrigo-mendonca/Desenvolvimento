using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpBook.Classes;

namespace SharpBook.Telas
{
    
    public partial class FormPerfil : Form
    {
        public Usuario uPerfil = null;
        public FormPerfil(Usuario tuPerfil)
        {
            InitializeComponent();
            uPerfil = tuPerfil;
        }

        private void FormPerfil_Load(object sender, EventArgs e)
        {
            Inicio();
        }

        public void Inicio()
        {
            picPerfil.Image = uPerfil.iFoto;
            cmdPerfil.Text  = uPerfil.cNome;
            lblNome.Text    = uPerfil.cNome + " " + uPerfil.cSobreNome;

            if (uPerfil.cLogin != Dados.oUsuLogado.cLogin)
            {
                edtEditPerfil.Visible = false;
            }
            gridAmigos.Columns.Clear();

            uPerfil.lAmigos.Add(uPerfil);    
            Dados.EditGrid(gridAmigos, uPerfil.lAmigos);
        }
        private void FormPerfil_Deactivate(object sender, EventArgs e)
        {
            
        }

        private void edtEditPerfil_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EditPerfil oEditPerf = new EditPerfil(Dados.oUsuLogado);
            oEditPerf.ShowDialog();
            uPerfil = Dados.oUsuLogado;
            Inicio();
        }

        private void cmdAmigos_Click(object sender, EventArgs e)
        {
            gridAmigos.Visible = true;
            gridMsg.Visible = false;
        }

        private void cmdMsg_Click(object sender, EventArgs e)
        {
            gridAmigos.Visible = false;
            gridMsg.Visible = true;
        }
    }
}