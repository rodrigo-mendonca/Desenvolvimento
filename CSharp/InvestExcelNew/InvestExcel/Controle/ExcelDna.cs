using System;
using ExcelDna.Integration;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SQL;
using Microsoft.Office.Interop.Excel;
using System.ComponentModel;

namespace InvestExcel
{
    public static class ExcelDna 
    {
        static Queue<ExcelReference> ResizeJobs = new Queue<ExcelReference>();

        public static void AutoOpen()
        {
            XlCall.Excel(XlCall.xlcOnSheet, null, "Recalculate");
        }
        public static void Recalculate()
        {
            Application app = (Application)ExcelDnaUtil.Application;
            app.CalculateFullRebuild();
        }

        public static object[,] Params(object tArg)
        {
            object[,] oRes;

            // caso o parametro não seja uma celula do excel, retorna o valor
            if (tArg.GetType() != typeof(ExcelReference))
            {
                oRes = new object[1, 1];
                oRes[0,0] = tArg;
                return oRes;
            }

            try
            {
                int nMaxI = 1, nMaxJ = 1;
                ExcelReference oRef = (ExcelReference)tArg;

                int nRows = oRef.RowLast - oRef.RowFirst + 1;
                int nColl = oRef.ColumnLast - oRef.ColumnFirst + 1;

                if (nRows > 1000 || nColl > 1000)
                    throw new Exception("O limite é 1000 linhas e 1000 colunas!");

                oRes = new object[nRows, nColl];
                for (int i = 0; i < nRows; i++)
                {
                    for (int j = 0; j < nColl; j++)
                    {
                        ExcelReference cellRef = new ExcelReference(
                            oRef.RowFirst + i, oRef.RowFirst + i,
                            oRef.ColumnFirst + j, oRef.ColumnFirst + j,
                            oRef.SheetId);

                        if (typeof(ExcelEmpty) != cellRef.GetValue().GetType())
                        {
                            oRes[i, j] = cellRef.GetValue();
                            nMaxI = i+1;
                            nMaxJ = j+1;
                        }
                        else
                            oRes[i, j] = ExcelEmpty.Value;
                    }
                }
                oRes = SysFuncoes.RedimMatriz(oRes, nMaxI, nMaxJ);
                return oRes;
            }
            catch (Exception e)
            {
                oRes = new object[1, 1];
                oRes[0, 0] = "ERRO: "+e.Message;
                return oRes;
            }
        }

        public static object Return(object[,] tRetorno)
        {
            return XlCall.Excel(XlCall.xlUDF, "Resize", tRetorno);
        }

        public static object[,] ListToMatriz(List<List<object>> lList)
        {
            object[,] oRetorno = new object[lList.Count, lList[0].Count];

            for (int i = 0; i < lList.Count; i++)
            {
                for (int j = 0; j < lList[i].Count; j++)
                {
                    // REMOVE OS ESPAÇOS EM BRANCO QUANDO STRING
                    if (lList[i][j].GetType() == typeof(string))
                        oRetorno[i, j] = lList[i][j].ToString().Trim();
                    else
                        oRetorno[i, j] = lList[i][j];
                }
            }

            return oRetorno;
        }

        public static object Resize(object[,] array)
        {
            ExcelReference caller = XlCall.Excel(XlCall.xlfCaller) as ExcelReference;
            return Resize(array, caller);
        }

        public static object Resize(object[,] array, ExcelReference caller)
        {
            if (caller == null)
                return ExcelError.ExcelErrorNA;

            int rows = array.GetLength(0);
            int columns = array.GetLength(1);

            if ((caller.RowLast - caller.RowFirst + 1 != rows) ||
                (caller.ColumnLast - caller.ColumnFirst + 1 != columns))
            {
                EnqueueResize(caller, rows, columns);
                ExcelAsyncUtil.QueueAsMacro(DoResizing);
            }

            return array;
        }

        static void EnqueueResize(ExcelReference caller, int rows, int columns)
        {
            ExcelReference target = new ExcelReference(caller.RowFirst, caller.RowFirst + rows - 1, caller.ColumnFirst, caller.ColumnFirst + columns - 1, caller.SheetId);
            ResizeJobs.Enqueue(target);
        }
        
        static void DoResizing()
        {
            while (ResizeJobs.Count > 0)
            {
                DoResize(ResizeJobs.Dequeue());
            }
        }
        
        static void DoResize(ExcelReference target)
        {
            object oldEcho = XlCall.Excel(XlCall.xlfGetWorkspace, 40);
            object oldCalculationMode = XlCall.Excel(XlCall.xlfGetDocument, 14);
            try
            {
                XlCall.Excel(XlCall.xlcEcho, false);
                XlCall.Excel(XlCall.xlcOptionsCalculation, 3);

                string formula = (string)XlCall.Excel(XlCall.xlfGetCell, 41, target);
                ExcelReference firstCell = new ExcelReference(target.RowFirst, target.RowFirst, target.ColumnFirst, target.ColumnFirst, target.SheetId);

                bool isFormulaArray = (bool)XlCall.Excel(XlCall.xlfGetCell, 49, target);
                if (isFormulaArray)
                {
                    object oldSelectionOnActiveSheet = XlCall.Excel(XlCall.xlfSelection);
                    object oldActiveCell = XlCall.Excel(XlCall.xlfActiveCell);

                    string firstCellSheet = (string)XlCall.Excel(XlCall.xlSheetNm, firstCell);
                    XlCall.Excel(XlCall.xlcWorkbookSelect, new object[] { firstCellSheet });
                    object oldSelectionOnArraySheet = XlCall.Excel(XlCall.xlfSelection);
                    XlCall.Excel(XlCall.xlcFormulaGoto, firstCell);

                    XlCall.Excel(XlCall.xlcSelectSpecial, 6);
                    ExcelReference oldArray = (ExcelReference)XlCall.Excel(XlCall.xlfSelection);

                    oldArray.SetValue(ExcelEmpty.Value);
                    XlCall.Excel(XlCall.xlcSelect, oldSelectionOnArraySheet);
                    XlCall.Excel(XlCall.xlcFormulaGoto, oldSelectionOnActiveSheet);
                }
                
                bool isR1C1Mode = (bool)XlCall.Excel(XlCall.xlfGetWorkspace, 4);
                string formulaR1C1 = formula;
                if (!isR1C1Mode)
                {
                    formulaR1C1 = (string)XlCall.Excel(XlCall.xlfFormulaConvert, formula, true, false, ExcelMissing.Value, firstCell);
                }
                
                object ignoredResult;
                XlCall.XlReturn retval = XlCall.TryExcel(XlCall.xlcFormulaArray, out ignoredResult, formulaR1C1, target);

                if (retval != XlCall.XlReturn.XlReturnSuccess)
                {
                    firstCell.SetValue("'" + formula);
                }
            }
            finally
            {
                XlCall.Excel(XlCall.xlcEcho, oldEcho);                
                XlCall.Excel(XlCall.xlcOptionsCalculation, oldCalculationMode);
            }
        }
    }
}
