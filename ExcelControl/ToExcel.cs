using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace ExcelControl
{
    public class ToExcel
    {
        #region Excel Export

        /// <summary>
        /// Data Model To Excel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="data"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public byte[] DataToExcel<T>(T data, string title)
        {
            if (data is IList)
            {
                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                {
                    SpreadsheetDocument spreadsheet = ExcelProc.CreateWorkbook(stream);
                    ExcelProc.AddBasicStyles(spreadsheet);
                    ExcelProc.AddAdditionalStyles(spreadsheet);

                    if (title.Length > 31)
                    {
                        title = title.Substring(0, 28) + "...";
                    }
                    ExcelProc.AddWorksheet(spreadsheet, title);
                    Worksheet worksheet = spreadsheet.WorkbookPart.WorksheetParts.First().Worksheet;

                    IList excelData = (data as IList);
                    if (excelData.Count > 0)
                    {
                        IList<PropertyInfo> PropertiesList = new List<PropertyInfo>();
                        int colIdx = 0;

                        foreach (var p in excelData[0].GetType().GetProperties())
                        {
                            if (p.GetCustomAttributes(true).Contains(new ExcelExceptDataAttribute()) == false)
                            {
                                PropertiesList.Add(p);

                                // 
                                var display = p.GetCustomAttribute<DisplayAttribute>();

                                ExcelProc.SetColumnHeadingValue(spreadsheet, worksheet, Convert.ToUInt32(colIdx + 1), display.Name, false, false); // 컬럼 헤더 ####
                                //ExcelProc.SetColumnHeadingValue(spreadsheet, worksheet, Convert.ToUInt32(colIdx + 1), p.Name, false, false); // 컬럼 헤더 ####
                                ExcelProc.SetColumnWidth(worksheet, colIdx + 1, 25);
                                colIdx++;
                            }
                        }

                        //ExcelProc.SetCellValue(spreadsheet, worksheet, 1, 1, title + " (Restricted Document)", 4, false, false); //제목 ####

                        int rowIndex = 1;
                        foreach (var d in excelData)
                        {
                            colIdx = 0;
                            foreach (var p in PropertiesList)
                            {
                                ExcelProc.SetCellValue(spreadsheet, worksheet, Convert.ToUInt32(colIdx + 1), Convert.ToUInt32(rowIndex + 1), p.GetValue(d, null) == null ? string.Empty : p.GetValue(d, null).ToString(), false, false);
                                colIdx++;
                            }
                            rowIndex++;
                        }
                    }
                    worksheet.Save();
                    spreadsheet.Close();

                    byte[] buffer = stream.ToArray();
                    string filename = string.Format("{0}.xlsx", title);

                    return buffer;
                    //return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);

                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Data Model To Excel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="data"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public void DataToExcelEx(List<Dictionary<string, object>> data, string title, List<GridHeaderModel> headers)
        {
            if (data != null)
            {
                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                {
                    int colIdx = 0;
                    SpreadsheetDocument spreadsheet = ExcelProc.CreateWorkbook(stream);
                    ExcelProc.AddBasicStyles(spreadsheet);
                    ExcelProc.AddAdditionalStyles(spreadsheet);

                    if (title.Length > 31)
                    {
                        title = title.Substring(0, 28) + "...";
                    }
                    ExcelProc.AddWorksheet(spreadsheet, title);
                    Worksheet worksheet = spreadsheet.WorkbookPart.WorksheetParts.First().Worksheet;

                    if (data.Count > 0)
                    {
                        // 타이틀 만들기
                        foreach (KeyValuePair<string, object> header in data[0])
                        {
                            foreach (var p in header.Value.GetType().GetProperties())
                            {
                                if (p.GetCustomAttributes(true).Contains(new ExcelExceptDataAttribute()) == false)
                                {
                                    var match = headers.Find(c => c.field == p.Name);
                                    if (match != null)
                                    {
                                        ExcelProc.SetColumnHeadingValue(spreadsheet, worksheet, Convert.ToUInt32(colIdx + 1), match.title, false, false);
                                        ExcelProc.SetColumnWidth(worksheet, colIdx + 1, 25);
                                        colIdx++;
                                    }
                                }
                            }
                        }

                        ExcelProc.SetCellValue(spreadsheet, worksheet, 1, 1, title + " (Restricted Document)", 4, false, false);

                        int rowIndex = 1;
                        // 내용 만들기
                        foreach (var list in data)
                        {
                            colIdx = 0;
                            foreach (KeyValuePair<string, object> body in list)
                            {
                                //colIdx = 0;
                                foreach (var p in body.Value.GetType().GetProperties())
                                {
                                    if (p.GetCustomAttributes(true).Contains(new ExcelExceptDataAttribute()) == false)
                                    {
                                        var match = headers.Find(c => c.field == p.Name);
                                        if (match != null)
                                        {
                                            ExcelProc.SetCellValue(spreadsheet, worksheet, Convert.ToUInt32(colIdx + 1), Convert.ToUInt32(rowIndex + 2), p.GetValue(body.Value, null) == null ? string.Empty : p.GetValue(body.Value, null).ToString(), false, false);
                                            colIdx++;
                                        }
                                    }
                                }
                            }
                            rowIndex++;
                        }
                    }
                    worksheet.Save();
                    spreadsheet.Close();

                    byte[] buffer = stream.ToArray();
                    string filename = string.Format("{0}.xlsx", title);

                    //return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);

                }
            }
            else
            {
                throw new Exception("Datatype Error");
            }
        }

        #endregion

    }

    /// <summary>
    /// Excel Except Data Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ExcelExceptDataAttribute : Attribute
    {
    }
}
