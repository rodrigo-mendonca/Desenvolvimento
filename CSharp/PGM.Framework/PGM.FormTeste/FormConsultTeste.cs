using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using PGM.Controls;
using PGM.SQL.Models;
using PGM.SQL.Repositories;
using PGM.Interfaces;
using PGM.Sys;
using PGM.Xml;

namespace PGM.FormTeste
{
    public partial class FormConsultTeste : FormConsult
    {
        public FormConsultTeste()
        {
            InitializeComponent();
        }

        private void FormConsultTeste_Load(object sender, EventArgs e)
        {

        }

        private void FormConsultTeste_PgmAmbient(object sender, EventArgs e)
        {
            IRepository<Teste> oInv = (IRepository<Teste>)PgmInjector.GetInstance<IRepository<Teste>>();

            List<Teste> all = oInv.Select().OrderBy(i => i.Desc).ToList();

            var oGrade = (from a in all
                          select new
                          {
                              a.PkId,
                              a.Desc,
                              a.DhAlteracao,
                              Inativo = a.Inativo == 1 ? "Sim" : "Não"
                          }).ToList();

            List<PgmHeader> oHeader = new List<PgmHeader>
            {
                new PgmHeader{FieldName = "PkId"        ,ColumnName = "ID"                        },
                new PgmHeader{FieldName = "Desc"        ,ColumnName = "Desc."                     },
                new PgmHeader{FieldName = "DhAlteracao" ,ColumnName = "Dh. Alteração",Format = "d"},
                new PgmHeader{FieldName = "Inativo"     ,ColumnName = "Inativo"                   }
            };

            PgmContextMenu oMenu = new PgmContextMenu();
            oMenu.AddMenu("Incluir", "PGM.FormTeste.FormDigTeste", this, 0);
            oMenu.AddMenu("Alterar", "PGM.FormTeste.FormDigTeste", this, this.GetGrid());
            oMenu.AddMenu("Consultar", "PGM.FormTeste.FormConsultTeste");

            ConfigFrm(all, oHeader, oMenu);
        }

        private void pgmButton1_Click(object sender, EventArgs e)
        {

            SaveFileDialog oSave = new SaveFileDialog();
            oSave.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            oSave.ShowDialog();

            if (oSave.FileName != "")
            {
                Repository<Teste> oInv = (Repository<Teste>)PgmInjector.GetInstance<IRepository<Teste>>();

                List<Teste> all = oInv.Select(i=>i.Exemplos).OrderBy(i => i.Desc).ToList();

                PgmXml oXml = new PgmXml();

                oXml.Serialize(all, oSave.FileName);
            }
        }

        private void pgmButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog oOpen = new OpenFileDialog();
            oOpen.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            oOpen.ShowDialog();

            if (oOpen.FileName != "")
            {
                PgmXml oXml = new PgmXml();

                List<Teste> row = (List<Teste>)oXml.Deserialize<List<Teste>>(oOpen.FileName);

                ConfigFrm(row);
            }

        }

        private void pgmButton3_Click(object sender, EventArgs e)
        {
            Exemplo oExe = new Exemplo();

            List<Exemplo> oExemplos = new List<Exemplo>();
            oExe = new Exemplo();
            oExe.PkId = "Id21";
            oExe.Desc = "Teste1";
            oExemplos.Add(oExe);
            oExe = new Exemplo();
            oExe.PkId = "Id22";
            oExe.Desc = "Teste2";
            oExemplos.Add(oExe);
            oExe = new Exemplo();
            oExe.PkId = "Id23";
            oExe.Desc = "Teste3";
            oExemplos.Add(oExe);
            oExe = new Exemplo();
            oExe.PkId = "Id24";
            oExe.Desc = "Teste4";
            oExemplos.Add(oExe);
            oExe = new Exemplo();
            oExe.PkId = "Id25";
            oExe.Desc = "Teste5";
            oExemplos.Add(oExe);

            Repository<Teste> oInv = (Repository<Teste>)PgmInjector.GetInstance<IRepository<Teste>>();


            Teste oTes = new Teste 
            { 
                Desc = "Teste relacionamento",
                Valor = 0,
                Data = DateTime.Now,
                Exemplos = oExemplos

            
            
            
            };


            oInv.Insert(oTes);
        }
    }
}
