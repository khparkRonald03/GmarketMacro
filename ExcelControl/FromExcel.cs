using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelControl
{
    public class FromExcel
    {
        public DataTable OpenExcel(string filePath, ref string err)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(filePath, false))
                {
                    WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                    IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                    string relationshipId = sheets.First().Id.Value;
                    WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
                    Worksheet workSheet = worksheetPart.Worksheet;
                    SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                    IEnumerable<Row> rows = sheetData.Descendants<Row>();

                    foreach (Cell cell in rows.ElementAt(0))
                    {
                        dt.Columns.Add(GetCellValue(spreadSheetDocument, cell));
                    }

                    var bodyRows = rows.ToList();

                    for (int Idx = 1; Idx < bodyRows.Count(); Idx++)
                    {
                        DataRow tempRow = dt.NewRow();

                        for (int i = 0; i < bodyRows[Idx].Descendants<Cell>().Count(); i++)
                        {
                            Cell cell = bodyRows[Idx].Descendants<Cell>().ElementAt(i);
                            if (cell == null)
                                break;

                            tempRow[i] = GetCellValue(spreadSheetDocument, cell);
                        }

                        dt.Rows.Add(tempRow);
                    }

                }
            }
            catch (Exception e)
            {
                err = e.Message;
                return null;
            }

            return dt;
        }

        private int CellReferenceToIndex(Cell cell)
        {
            int index = 0;
            string reference = cell.CellReference.ToString().ToUpper();
            foreach (char ch in reference)
            {
                if (Char.IsLetter(ch))
                {
                    int value = (int)ch - (int)'A';
                    index = (index == 0) ? value : ((index + 1) * 26) + value;
                }
                else
                    return index;
            }
            return index;
        }

        private string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            if (cell.CellValue == null)
                return string.Empty;

            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;

            string value = cell.CellValue.InnerXml;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            else
            {
                return value;
            }
        }
    }
}
