using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InvestAddinCore.Classes;
using Microsoft.Office.Tools.Ribbon;
using System.Data;
using Microsoft.Office.Tools.Excel;
using Excel = Microsoft.Office.Interop.Excel;

namespace InvestAddinCore
{
    public class Core
    {
        public Core()
        {

        }

        public static void PreencheFundos(RibbonDropDown Combo, RibbonFactory Ribon)
        {
            DataTable tabela = new DataTable();

            string comando = "SELECT PK_ID,DS_FANTASIA " +
                             "FROM TCLUCLUBE " +
                             "WHERE TG_INATIVO = 0 [[CLUBES]] " +
                             "ORDER BY DS_FANTASIA";

            direito direito = new direito();

            string clubes = direito.clubes();

            comando = comando.Replace("[[CLUBES]]", " AND PK_ID IN (" + clubes + ") ");

            BaseDados.ExecutaSQL(comando, tabela);

            Combo.Items.Clear();

            // PREENCHE A COMBO COM TODOS OS FUNDOS
            foreach (DataRow Row in tabela.Rows)
            {
                RibbonDropDownItem item = Ribon.CreateRibbonDropDownItem(); ;
                
                item.Label = Row[1].ToString();

                Combo.Items.Add(item);
            }
        }

        public static void BuscaInfo(string Data, string Fundo,RibbonMenu Menu,Excel._Application App)
        {
            DataTable tabela = new DataTable();

            //CRIA QUERY COM BASE EM CAMPOS SELECIONADOS
            Ativo ativo = new Ativo();
            string comando = ativo.montaquery(Menu);

            comando = comando.Replace("[[DATA]]", Data);
            comando = comando.Replace("[[CLUBE]]", Fundo);

            if (string.IsNullOrEmpty(comando))
            {
                return;
            }
            BaseDados.ExecutaSQL(comando, tabela);

            // CRIA LINHA DE TOTAL
            if (comando.IndexOf("CAR.VL_PATRIMONIO") >= 0)
            {
                decimal sum = tabela.AsEnumerable().Sum(m => m.Field<decimal>("VL_PATRIMONIO"));
                tabela.Rows.Add("Total", " ", sum);
            }

            string[] titulo = new string[99];
            int i = 0;
            foreach (RibbonCheckBox check in Menu.Items)
            {
                if (check.Checked)
                {
                    titulo[i] = check.Label;
                    i++;
                }
            }
            //ADIONA O TIULO PARA CAMPOS A PLANILHA
            AdicionaTitulo(titulo,App);
            //PRENCHE OS DADOS IGNORANDO A PRIMEIRA LINHA SELECIONADA
            PreenchePlanilha(tabela, App);
        }

        public static void AdicionaTitulo(string[] Titulo, Excel._Application Application)
        {
            Excel.Worksheet xlWorkSheet;

            xlWorkSheet = Application.ActiveSheet;
            Excel.Range range = Application.ActiveCell;

            if (xlWorkSheet == null)
            {
                return;
            }

            int column = range.Column;
            int row = range.Row;

            int count = 0;

            count = Titulo.Count();

            for (int i = 1; i <= count; i++)
            {
                if (string.IsNullOrEmpty(Titulo[i - 1]))
                {
                    return;
                }
                xlWorkSheet.Cells[row, i + (column - 1)].Value = Titulo[i - 1];
            }        
        }

        public static void PreenchePlanilha(DataTable Table, Excel._Application Application)
        {

            Excel.Worksheet xlWorkSheet;

            xlWorkSheet = Application.ActiveSheet;
            Excel.Range range = Application.ActiveCell;

            if (xlWorkSheet == null)
            {
                return;
            }

            int column = range.Column;
            int row = range.Row + 1;

            int rownum = row;

            //PREENCHE A COMBO COM TODOS OS FUNDOS
            foreach (DataRow Row in Table.Rows)
            {
                for (int i = 1; i <= (Table.Columns.Count); i++)
                {
                    xlWorkSheet.Cells[rownum, i + (column - 1)].Value = Row[i - 1];
                }
                rownum++;
            }
            xlWorkSheet.Columns.AutoFit();
        }
    }
}
