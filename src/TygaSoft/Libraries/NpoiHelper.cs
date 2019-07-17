using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace TygaSoft.WebHelper
{
    public class NpoiHelper
    {
        public DataTable GetExcelData(string ext, Stream stream)
        {
            try
            {
                var dt = new DataTable();

                IWorkbook wb = null;
                ISheet sheet = null;

                wb = ext.ToLower() == ".xls" ? new HSSFWorkbook(stream) as IWorkbook : new XSSFWorkbook(stream) as IWorkbook;
                sheet = wb.GetSheetAt(0);

                var headRow = sheet.GetRow(sheet.FirstRowNum);
                var headCells = headRow.Cells;
                var totalCol = headCells.Count;
                foreach (var item in headCells)
                {
                    dt.Columns.Add(new DataColumn(item.RichStringCellValue.String.Replace("*",""), System.Type.GetType("System.String")));
                }
                sheet.RemoveRow(headRow);
                var rows = sheet.GetRowEnumerator();
                List<ICell> cells;
                while (rows.MoveNext())
                {
                    var dr = dt.NewRow();
                    var row = rows.Current as IRow;
                    cells = row.Cells;
                    var isBlankRow = true;

                    foreach (var cell in cells)
                    {
                        switch (cell.CellType)
                        {
                            case CellType.Numeric:
                                isBlankRow = false;
                                dr[cell.ColumnIndex] = cell.NumericCellValue;
                                break;
                            case CellType.String:
                                isBlankRow = false;
                                dr[cell.ColumnIndex] = cell.StringCellValue;
                                break;
                            case CellType.Blank:
                                dr[cell.ColumnIndex] = "";
                                break;
                            default:
                                throw new ArgumentException("获取到的数据存在异常，请下载模板并正确填充数据");
                        }
                    }
                    if(!isBlankRow) dt.Rows.Add(dr);
                }

                return dt;
            }
            catch
            {
                throw;
            }
        }

        public string ExportExcel(DataTable dt,string excelFileName)
        {
            TempFolder temp = new TempFolder();
            temp.DoTempFile();
            var tempUrl = temp.GetExportTempUrl(excelFileName);
            var sourceUrl = temp.GetExportSourceUrl(excelFileName);
            //var tempUrl = "";
            //temp.CopyToTemp(excelFileName,out tempUrl);
            SetExcelData(dt, HttpContext.Current.Server.MapPath(sourceUrl), HttpContext.Current.Server.MapPath(tempUrl));

            return tempUrl.Replace("~","");
        }

        public void SetExcelData(DataTable dt, string sourceUrl, string fileName)
        {
            var ext = VirtualPathUtility.GetExtension(sourceUrl);
            FileStream stream = File.OpenRead(sourceUrl);
            SetExcelData(dt, ext, stream, fileName);
            //stream.Flush();
            stream.Close();
        }

        public void SetExcelData(DataTable dt,string ext, Stream stream,string fileName)
        {
            IWorkbook wb = null;
            ISheet sheet = null;
            wb = ext.ToLower() == ".xls" ? new HSSFWorkbook(stream) as IWorkbook : new XSSFWorkbook(stream) as IWorkbook;
            sheet = wb.GetSheetAt(0);
            var headRow = sheet.GetRow(sheet.FirstRowNum);
            int headRowIndex = headRow.RowNum;
            var headCells = headRow.Cells;

            var cols = dt.Columns;
            for (var i = 0; i < cols.Count; i++) {
                headCells[i].SetCellValue(cols[i].ColumnName);
            }
            var drc = dt.Rows;
            foreach (DataRow dr in drc)
            {
                headRowIndex++;
                var newRow = sheet.CreateRow(headRowIndex);
                for (var i = 0; i < cols.Count; i++)
                {
                    var cell = newRow.CreateCell(i);
                    cell.SetCellValue(dr[i].ToString());
                }
            }

            var dir = Path.GetDirectoryName(fileName);

            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            using (FileStream file = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                wb.Write(file);
            }
        }

    }
}
