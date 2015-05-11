using System;
using System.Windows.Forms;
using ExcelDna.Integration;
using InvestExcel.Controle;
// This class implements the ExcelDna.Integration.IExcelAddIn interface.
// This allows the add-in to run code at start-up and shutdown.
namespace InvestExcel
{
    public class Invest : IExcelAddIn
    {
        public void AutoOpen()
        {
            //ExcelDna.AutoOpen();
        }

        public void AutoClose()
        {
            
        }
    }
}