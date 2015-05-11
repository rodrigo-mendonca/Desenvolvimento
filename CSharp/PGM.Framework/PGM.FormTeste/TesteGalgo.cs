using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PGM.Controls;
using PGM.SQL.Repositories;
using PGM.Interfaces;
using PGM.SQL.Models;
using PGM.Sys;

namespace PGM.FormTeste
{
    public partial class TesteGalgo : FormConsult
    {
        public TesteGalgo()
        {
            InitializeComponent();
        }

        private void TesteGalgo_Load(object sender, EventArgs e)
        {

        }

        private void TesteGalgo_PgmAmbient(object sender, EventArgs e)
        {
            

            Repository<Galgo> oRepo = (Repository<Galgo>)PgmInjector.GetInstance<IRepository<Galgo>>();

            List<Galgo> oGrade = oRepo.SelectAll().Take(500).ToList();

            ConfigFrm(oGrade);
        }
    }
}
