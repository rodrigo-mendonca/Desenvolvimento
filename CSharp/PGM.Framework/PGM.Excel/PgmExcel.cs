using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Reflection;
using oExcel = Microsoft.Office.Interop.Excel;
using PGM.Extensions.Pgm;

namespace PGM.Excel
{
    public class PgmExcel
    {
        public bool Visible = false;
        List<PgmExcelHeader> oHeader = new List<PgmExcelHeader>();
        oExcel._Application oApp;
        oExcel._Workbook oWork;
        oExcel.Worksheet oSheet;
        /// <summary>
        /// Abre um excel novo
        /// </summary>
        public PgmExcel()
        {
            oApp = new oExcel.Application();
            oWork = oApp.Workbooks.Add();
            oSheet = oWork.Sheets.Add();
        }
        /// <summary>
        /// Recebe o excel e usa o WorkBook e Sheet atual
        /// </summary>
        /// <param name="tApp">Objeto Application</param>
        public PgmExcel(oExcel._Application tApp)
        {
            oApp = tApp;
            oWork = oApp.ActiveWorkbook;
            oSheet = oApp.ActiveSheet;
        }
        /// <summary>
        /// Recebe o excel e o WorkBook e usa o Sheet atual
        /// </summary>
        /// <param name="tApp">Objeto Application</param>
        /// <param name="tWork">Objeto Workbook</param>
        public PgmExcel(oExcel._Application tApp,oExcel._Workbook tWork)
        {
            oApp    = tApp;
            oWork   = tWork;
            oSheet = oApp.ActiveSheet;
        }
        /// <summary>
        /// Recebe todos os parametros
        /// </summary>
        /// <param name="tApp">Objeto Application</param>
        /// <param name="tWork">Objeto Workbook</param>
        /// <param name="tSheet">Objeto Sheet</param>
        public PgmExcel(oExcel._Application tApp, oExcel._Workbook tWork, oExcel.Worksheet tSheet)
        {
            oApp = tApp;
            oWork = tWork;
            oSheet = tSheet;
        }
        /// <summary>
        /// Altera a aba selecionada
        /// </summary>
        /// <param name="tSheet">Obj da Sheet</param>
        public void SetSheet(oExcel.Worksheet tSheet)
        {
            oSheet = tSheet;
        }
        /// <summary>
        /// Salva o arquivo 
        /// </summary>
        /// <param name="tPath">Caminho para salvar o arquivo</param>
        public void Save(string tPath)
        {
            oWork.SaveAs(tPath);
            Close();
        }
        /// <summary>
        /// Fecha o workbook e o excel
        /// </summary>
        public void Close()
        {
            oWork.Close();
            oApp.Quit();
        }

        /// <summary>
        /// Exporta a lista generica de acordo com o Header, caso o Header não for passado é usado o nome do campo na lista
        /// </summary>
        /// <typeparam name="T">Tipo da lista</typeparam>
        /// <param name="tList">Lista com os dados</param>
        public void Export<T>(IList<T> tList)
        {
            // ignora lista vazia
            if (tList.Count == 0)
                return;

            // cria o layout se não foi passado
            if (oHeader.Count == 0)
                CreateHeader(tList);

            oApp.Visible = Visible;
            int nC = 1;
            // cria a matriz que será usada para lançar os dados no excel
            object[,] oExport = new object[tList.Count, oHeader.Count];

            oExcel.Range oC1, oC2,oRange;
            oC1 = (oExcel.Range)oSheet.Cells[1, 1];
            oC2 = (oExcel.Range)oSheet.Cells[1, oHeader.Count];
            oRange = oSheet.get_Range(oC1, oC2);

            int nCor = 0;
            // cor padrão do titulo
            oRange.Interior.Color = nCor.ToRgb(192, 192, 192);
            oRange.Font.Bold = true;

            // para cada coluna de layout
            foreach (var oH in oHeader)
            {
                // pegar o index do item da lista
                nC = oHeader.IndexOf(oH) + 1;

                oC1 = (oExcel.Range)oSheet.Cells[1, nC];
                oC2 = (oExcel.Range)oSheet.Cells[tList.Count + 1, nC];
                oRange = oSheet.get_Range(oC1, oC2);

                PropertyInfo oProper = typeof(T).GetProperty(oH.FieldName);
                
                oC1.Value = oH.ColumnName;

                // formata o campo de acordo com o header
                oRange.NumberFormat = oH.Format;

                // começar o final da lista
                int j = tList.Count - 1;
                // prenche os valores da coluna para a matriz
                for (int i = 0; i < tList.Count; i++)
                {
                    // começo da lista
                    oExport[i, nC - 1] = oProper.GetValue(tList[i]);
                    // final da lista
                    oExport[j, nC - 1] = oProper.GetValue(tList[j]);
                    j--;
                    // verifica se os indices se encontraram, se sim, sai do loop
                    if (i == j)
                        break;
                }
            }

            oC1 = (oExcel.Range)oSheet.Cells[2, 1];
            oC2 = (oExcel.Range)oSheet.Cells[tList.Count + 1, oHeader.Count];
            oRange = oSheet.get_Range(oC1, oC2);

            oRange.Value = oExport;
            oRange.EntireColumn.AutoFit();
        }
        /// <summary>
        /// Cria um header com os nomes dos campos da lista
        /// </summary>
        /// <param name="tList">Lista com os dados</param>
        private void CreateHeader<T>(IList<T> tList)
        {
            string cFormat;

            foreach (PropertyInfo oP in typeof(T).GetProperties())
            {
                cFormat = "";
                if (oP.PropertyType == typeof(int))
                    cFormat = "#0";
                if (oP.PropertyType == typeof(double) || oP.PropertyType == typeof(decimal))
                    cFormat = "#,##0.00";

                oHeader.Add(new PgmExcelHeader() 
                { 
                    FieldName = oP.Name, 
                    ColumnName = oP.Name,
                    Format = cFormat
                });
            }
        }
    }
}
