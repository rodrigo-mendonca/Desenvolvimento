using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Configuration;
using System.Windows.Forms;
using PGM.SQL.Models;
using PGM.SQL.Repositories;
using PGM.Controls;
using PGM.Extensions.FoxPro;
using PGM.Extensions.Pgm;
using System.Drawing;
using PGM.Interfaces;
using PGM.Sys;
using PGM.Excel;

namespace PGM.FormTeste
{
    public partial class FormTeste : FormBase
    {
        public FormTeste()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ISysRepository<AjudaSistema> oInv = (ISysRepository<AjudaSistema>)PgmInjector.GetInstance<IRepository<AjudaSistema>>();
            oInv.SetConnection(PgmGlobal.DbSysConnectName);
            List<AjudaSistema> all = oInv.SelectAll().OrderBy(i => i.Name).ToList();

            Grid.SetDataSource(all);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IRepository<Teste> repo = (IRepository<Teste>)PgmInjector.GetInstance<IRepository<Teste>>();
            
            var novo = new Teste()
            {
                PkId = 0,
                Desc = txtDesc.Text,
                Valor = Convert.ToDecimal(txtValor.Text),
                Data = Convert.ToDateTime(txtData.Text)
            };
            repo.Insert(novo);

            button1_Click(sender,e);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Teste oRow =  Grid.GetSelectRow<Teste>();
            txtID.SetValue(oRow.PkId);
            txtDesc.SetValue(oRow.Desc);
            txtValor.SetValue(oRow.Valor);
            txtData.SetValue(oRow.Data);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            IRepository<Teste> oRepo = (IRepository<Teste>)PgmInjector.GetInstance<IRepository<Teste>>();
            
            int nID = Convert.ToInt16(txtID.Text);

            Teste oTes = oRepo.SelectId(nID);

            oTes.Desc = txtDesc.Text;
            oTes.Valor = (decimal)txtValor.GetValue();
            oTes.Data = (DateTime)txtData.GetValue();

            oRepo.Update(oTes);
            button1_Click(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            IRepository<Teste> oRepo = (IRepository<Teste>)PgmInjector.GetInstance<IRepository<Teste>>();
            
            int nID = Convert.ToInt16(txtID.Text);

            Teste oTes = oRepo.SelectId(nID);
            oRepo.Delete(oTes);

            
            button1_Click(sender, e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DateTime dDe = Convert.ToDateTime(txtUtil.Text);
            bool lTeste = dDe.VDiaUtil();

            MessageBox.Show(lTeste ? "Util" : "Não Util");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DateTime dDe = Convert.ToDateTime(txtUtil.Text);
            dDe = dDe.ProxUtil(Convert.ToInt16(txtDiasUteis.Text));

            MessageBox.Show(dDe.ToString());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DateTime dDe = Convert.ToDateTime(txtUtil.Text);
            DateTime dAte = Convert.ToDateTime(txtAte.Text);
            int nCount = dDe.ContaUteis(dAte, "BRA");

            MessageBox.Show(nCount.ToString());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int nNum = Convert.ToInt32(txtNumZero.Text);
            string cZero = nNum.StrZero(10);

            MessageBox.Show(cZero);
        }

        private void cmdCriptar_Click(object sender, EventArgs e)
        {
            string Crip = txtSenha.Text.Encrypt();

            MessageBox.Show(Crip);
        }

        private void pgmButton1_Click(object sender, EventArgs e)
        {
            Grid.Clear();
        }

        private void cmdExportar_Click(object sender, EventArgs e)
        {
            IRepository<InvestimentoPosicao> oRepo = (IRepository<InvestimentoPosicao>)PgmInjector.GetInstance<IRepository<InvestimentoPosicao>>();

            IList<InvestimentoPosicao> oInv = oRepo.Take(30000);

            PgmExcel oE = new PgmExcel();

            SaveFileDialog oSave = new SaveFileDialog();
            oSave.Filter = "xlsx|*.xlsx";
            oSave.ShowDialog();

            oE.Visible = true;
            oE.Export(oInv);
            oE.Save(oSave.FileName);
        }
    }
}
