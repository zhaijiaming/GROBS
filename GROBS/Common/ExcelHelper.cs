using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using NPOI.XSSF.UserModel;
using System.Web.UI.WebControls;

namespace GROBS.Common
{
    [Serializable]
    public class ExcelHelper
    {
        /// <summary>
        /// DataTable导出到Excel文件
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">保存位置</param> 
        public static void Export(DataTable dtSource, string headerText, string strFileName)
        {
            using (MemoryStream ms = Export(dtSource, headerText))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        /// <summary>
        /// DataSet导出到Excel文件
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strFileName">保存位置</param> 
        public static void ExportMul(DataSet dsSource, string strFileName)
        {
            if (dsSource == null) return;
            HttpContext curContext = HttpContext.Current;

            // 设置编码和附件格式
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode(strFileName + ".xls", Encoding.UTF8));

            curContext.Response.BinaryWrite(Export(dsSource).GetBuffer());
            curContext.Response.End();
        }

        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param> 
        public static MemoryStream Export(DataTable dtSource, string headerText)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            int sheetIndex = 1;
            ISheet sheet = workbook.CreateSheet("Sheet" + sheetIndex);

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = ""; //填加xls文件作者信息
                si.ApplicationName = ""; //填加xls文件创建程序信息
                si.LastAuthor = ""; //填加xls文件最后保存者信息
                si.Comments = ""; //填加xls文件作者信息
                si.Title = ""; //填加xls文件标题信息
                si.Subject = "";//填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            ICellStyle dateStyle = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-MM-dd");

            //取得列宽
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex % 65535 == 0 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheetIndex++;
                        sheet = workbook.CreateSheet("Sheet" + sheetIndex);
                    }

                    #region 表头及样式
                    {
                        if (!string.IsNullOrEmpty(headerText))
                        {
                            IRow headerRow = sheet.CreateRow(0);
                            headerRow.HeightInPoints = 15;
                            headerRow.CreateCell(0).SetCellValue(headerText);

                            ICellStyle headStyle = workbook.CreateCellStyle();
                            headStyle.Alignment = HorizontalAlignment.Left;
                            IFont font = workbook.CreateFont();
                            font.FontHeightInPoints = 10;
                            font.Boldweight = 700;
                            headStyle.SetFont(font);
                            headStyle.VerticalAlignment = VerticalAlignment.Center;
                            headerRow.GetCell(0).CellStyle = headStyle;

                            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, dtSource.Columns.Count - 1));
                        }
                    }
                    #endregion


                    #region 列头及样式
                    {
                        IRow headerRow = sheet.CreateRow(string.IsNullOrEmpty(headerText) ? 0 : 1);
                        ICellStyle headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = HorizontalAlignment.Center;
                        IFont font = workbook.CreateFont();
                        font.FontHeightInPoints = 11;
                        font.Boldweight = 500;
                        headStyle.SetFont(font);
                        foreach (DataColumn column in dtSource.Columns)
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                            headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                            //设置列宽
                            int colWidth = arrColWidth[column.Ordinal];
                            colWidth = colWidth > 50 ? 50 : colWidth;
                            sheet.SetColumnWidth(column.Ordinal, (colWidth + 1) * 256);
                            //sheet.SetColumnWidth(column.Ordinal, 256 * 100);
                        }
                    }
                    rowIndex = string.IsNullOrEmpty(headerText) ? 1 : 2;
                    #endregion
                }
                #endregion


                #region 填充内容
                IRow dataRow = sheet.CreateRow(rowIndex);
                System.Text.RegularExpressions.Regex numReg = new System.Text.RegularExpressions.Regex(@"^[0-9]+(\.[0-9]+)?$");
                foreach (DataColumn column in dtSource.Columns)
                {
                    ICell newCell = dataRow.CreateCell(column.Ordinal);

                    string drValue = row[column].ToString();
                    if (drValue.StartsWith("0") && !drValue.StartsWith("0."))
                    {
                        newCell.SetCellValue(drValue);
                    }
                    //数字和小数
                    else if (drValue.Length < 12 & numReg.IsMatch(drValue))
                    {
                        double v = Convert.ToDouble(drValue);
                        newCell.SetCellValue(v);
                    }
                    else if (drValue == "0")
                    {
                        newCell.SetCellValue(0);
                    }
                    else
                    {
                        switch (column.DataType.ToString())
                        {
                            case "System.String"://字符串类型
                                newCell.SetCellValue(drValue);
                                break;
                            case "System.DateTime"://日期类型
                                DateTime? dateV = Convert.ToDateTime(drValue);
                                if (dateV != null)
                                {
                                    newCell.SetCellValue(dateV.Value);
                                    newCell.CellStyle = dateStyle;//格式化显示
                                }
                                else
                                {
                                    newCell.SetCellValue(string.Empty);
                                }
                                break;
                            case "System.Boolean"://布尔型
                                bool boolV = false;
                                bool.TryParse(drValue, out boolV);
                                newCell.SetCellValue(boolV);
                                break;
                            case "System.Int16"://整型
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int intV = 0;
                                int.TryParse(drValue, out intV);
                                newCell.SetCellValue(intV);
                                break;
                            case "System.Decimal"://浮点型
                            case "System.Double":
                                double doubV = 0;
                                double.TryParse(drValue, out doubV);
                                if (doubV != 0)
                                    newCell.SetCellValue(doubV);
                                break;
                            case "System.DBNull"://空值处理
                                newCell.SetCellValue("");
                                break;
                            default:
                                newCell.SetCellValue("");
                                break;
                        }
                    }
                }
                #endregion

                rowIndex++;
            }


            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }

        }

        public static void ExportByWeb(DataTable dtSource, string strFileName)
        {
            ExportByWeb(dtSource, string.Empty, strFileName);
        }

        /// <summary>
        /// 用于Web导出
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">文件名</param> 
        public static void ExportByWeb(DataTable dtSource, string headerText, string strFileName)
        {
            if (dtSource == null) throw new ArgumentNullException("dtSource");
            HttpContext curContext = HttpContext.Current;

            // 设置编码和附件格式
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode(strFileName + ".xls", Encoding.UTF8));

            curContext.Response.BinaryWrite(Export(dtSource, headerText).GetBuffer());
            curContext.ApplicationInstance.CompleteRequest();
            //curContext.Response.End();
        }

        /// <summary>
        /// 读取excel 默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <param name="chkEmpty">是否校验列和行的第一个单元格为空，如果开启，由发现为空，将不会读取</param>
        /// <returns></returns>
        public static DataTable Import(string strFileName, bool chkEmpty = false, int headRowIndex = 0)
        {
            return Import(strFileName, chkEmpty, 0, headRowIndex, headRowIndex + 1);
        }

        /// <summary>
        /// 读取excel 默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <param name="chkEmpty">是否校验列和行的第一个单元格为空，如果开启，由发现为空，将不会读取</param>
        /// <returns></returns>
        public static DataTable Import(string strFileName, bool chkEmpty, int sheetIndex, int headRowIndex, int dataRowIndex)
        {
            DataTable dt = new DataTable();
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                IWorkbook hssfworkbook = WorkbookFactory.Create(file);

                XSSFFormulaEvaluator e = new XSSFFormulaEvaluator(hssfworkbook);
                ISheet sheet = hssfworkbook.GetSheetAt(sheetIndex);
                if (sheet == null)
                    return dt;

                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
                IRow headerRow = sheet.GetRow(headRowIndex);
                if (headerRow == null)
                    return dt;
                int cellCount = headerRow.LastCellNum;
                for (int j = 0; j < cellCount; j++)
                {
                    ICell cell = headerRow.GetCell(j);
                    if (cell == null)
                    {
                        dt.Columns.Add("");
                        continue;
                    }
                    if (chkEmpty)
                    {
                        if (string.IsNullOrEmpty(cell.ToString()))
                        {
                            continue;
                        }
                    }
                    dt.Columns.Add(cell.ToString());
                }


                bool isRowOK = true;
                int startIndex = headRowIndex + 1;
                if (startIndex == 0)
                {
                    startIndex = sheet.FirstRowNum + 1;
                }
                for (int i = dataRowIndex; i <= sheet.LastRowNum; i++)
                {
                    isRowOK = true;
                    IRow row = sheet.GetRow(i);
                    DataRow dataRow = dt.NewRow();
                    if (row == null)
                    {
                        continue;
                    }
                    int emptyCount = 0;
                    if (row.FirstCellNum < 0) continue;
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (chkEmpty && j == row.FirstCellNum)      //校验第一个单元格 
                        {
                            if (row.GetCell(j) == null || string.IsNullOrEmpty(row.GetCell(j).ToString()))
                            {
                                isRowOK = false;
                                break;
                            }
                        }

                        if (row.GetCell(j) != null)
                        {
                            dataRow[j] = CellValue(e, row.GetCell(j));
                        }

                        if (string.IsNullOrEmpty(dataRow[j].ToString()))
                            emptyCount++;
                    }
                    if (isRowOK && (row.FirstCellNum + emptyCount) < cellCount)
                    {
                        dt.Rows.Add(dataRow);
                    }
                }
                return dt;
            }
        }

        public static DataTable Import(string strFileName, Dictionary<string, int> dicColmun, int sheetIndex, int dataRowIndex)
        {
            DataTable dt = new DataTable();
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                IWorkbook hssfworkbook = WorkbookFactory.Create(file);
                XSSFFormulaEvaluator e = new XSSFFormulaEvaluator(hssfworkbook);

                ISheet sheet = hssfworkbook.GetSheetAt(sheetIndex);
                if (sheet == null)
                    return dt;

                foreach (var obj in dicColmun)
                {
                    dt.Columns.Add(obj.Key);
                }
                for (int i = dataRowIndex; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null)
                        continue;

                    DataRow dataRow = dt.NewRow();
                    int emptyCount = 0;
                    foreach (var itme in dicColmun)
                    {
                        ICell cell = row.GetCell(itme.Value);
                        dataRow[itme.Key] = CellValue(e, cell);

                        if (string.IsNullOrEmpty(dataRow[itme.Key].ToString()))
                            emptyCount++;
                    }
                    if (emptyCount < dicColmun.Count)
                        dt.Rows.Add(dataRow);
                }
                return dt;
            }
        }

        public static string GetCellValue(string strFileName, int rowIndex, int columnIndex, int sheetIndex = 0)
        {
            DataTable dt = new DataTable();
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                // HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                IWorkbook hssfworkbook = WorkbookFactory.Create(file);
                ISheet sheet = hssfworkbook.GetSheetAt(sheetIndex);
                if (sheet == null)
                    return string.Empty;

                IRow row = sheet.GetRow(rowIndex);
                if (row == null)
                    return string.Empty;

                ICell cell = row.GetCell(columnIndex);
                if (cell == null)
                    return string.Empty;

                XSSFFormulaEvaluator e = new XSSFFormulaEvaluator(hssfworkbook);
                object v = CellValue(e, cell);
                return v == null ? string.Empty : v.ToString();
            }
        }

        private static object CellValue(XSSFFormulaEvaluator e, ICell cell)
        {
            if (cell == null)
                return null;
            else
            {
                try
                {
                    if (cell.CellType == CellType.Boolean)
                        return cell.BooleanCellValue;
                    else if (cell.CellType == CellType.Error)
                        return cell.ErrorCellValue;
                    else if (cell.CellType == CellType.Numeric)
                    {
                        if (HSSFDateUtil.IsCellDateFormatted(cell))
                        {
                            return cell.DateCellValue;
                        }
                        else
                        {
                            return cell.NumericCellValue;
                        }
                    }
                    else if (cell.CellType == CellType.String)
                        return cell.StringCellValue;
                    else if (cell.CellType == CellType.Formula)
                        return e.EvaluateInCell(cell);
                    else
                        return cell.ToString();
                }
                catch
                {
                    return cell.ToString();
                }
            }
        }

        #region CMS系统扩展方法

        public static void ExportExcel(DataSet ds, string strFileName)
        {
            if (ds == null) throw new ArgumentNullException("DataSet Is Null");
            HttpContext curContext = HttpContext.Current;
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode(strFileName + ".xls", Encoding.UTF8));

            curContext.Response.BinaryWrite(Export(ds).GetBuffer());
            curContext.ApplicationInstance.CompleteRequest();

            //报错：“由于代码已经过优化或者本机框架位于调用堆栈之上，无法计算表达式的值。”
            //curContext.Response.End();
        }

        /// <summary>
        /// DataSet导出到Excel的MemoryStream
        /// </summary>
        /// <param name="DataSet">多个DataTable</param>
        public static MemoryStream Export(DataSet ds)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();

            #region 右击属性信息



            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Author = ""; //填加xls文件作者信息
            si.ApplicationName = ""; //填加xls文件创建程序信息
            si.LastAuthor = ""; //填加xls文件最后保存者信息
            si.Comments = ""; //填加xls文件作者信息
            si.Title = ""; //填加xls文件标题信息
            si.Subject = "导出数据";//填加文件主题信息
            si.CreateDateTime = DateTime.Now;
            workbook.SummaryInformation = si;

            #endregion

            //设置数据行样式
            ICellStyle dataStyle = SetDataStyle(workbook);

            //格式化日期样式
            ICellStyle dateStyle = SetDateStyle(workbook);

            ISheet sheet = null;
            int sheetIndex = 1;


            for (int i = 0; i < ds.Tables.Count; i++)
            {

                int rowIndex = 0;
                foreach (DataRow row in ds.Tables[i].Rows)
                {
                    #region excel2003行数超过65535行，新建sheet

                    if (rowIndex == 65535 || rowIndex == 0)
                    {
                        sheet = workbook.CreateSheet("Sheet" + sheetIndex);
                        //设置表头样式
                        SetHeadStyle(workbook, sheet, ds.Tables[i]);
                        sheetIndex++;
                        rowIndex = 1;
                    }

                    #endregion

                    #region 填充内容
                    IRow dataRow = sheet.CreateRow(rowIndex);
                    System.Text.RegularExpressions.Regex numReg = new System.Text.RegularExpressions.Regex(@"^[0-9]+(\.[0-9]+)?$");
                    foreach (DataColumn column in ds.Tables[i].Columns)
                    {
                        ICell newCell = dataRow.CreateCell(column.Ordinal);
                        newCell.CellStyle = dataStyle;

                        string drValue = row[column].ToString();
                        if (drValue.StartsWith("0") && !drValue.StartsWith("0."))
                        {
                            newCell.SetCellValue(drValue);
                        }
                        //数字和小数
                        else if (drValue.Length < 12 & numReg.IsMatch(drValue))
                        {
                            double v = Convert.ToDouble(drValue);
                            newCell.SetCellValue(v);
                        }
                        else if (drValue == "0")
                        {
                            newCell.SetCellValue(0);
                        }
                        else
                        {
                            switch (column.DataType.ToString())
                            {
                                case "System.String"://字符串类型
                                    newCell.SetCellValue(drValue);
                                    break;
                                case "System.DateTime"://日期类型
                                    DateTime? dateV = Convert.ToDateTime(drValue);
                                    if (dateV != null)
                                    {
                                        newCell.SetCellValue(dateV.Value);
                                        newCell.CellStyle = dateStyle;//格式化显示
                                    }
                                    else
                                    {
                                        newCell.SetCellValue(string.Empty);
                                    }
                                    break;
                                case "System.Boolean"://布尔型
                                    bool boolV = false;
                                    bool.TryParse(drValue, out boolV);
                                    newCell.SetCellValue(boolV);
                                    break;
                                case "System.Int16"://整型
                                case "System.Int32":
                                case "System.Int64":
                                case "System.Byte":
                                    int intV = 0;
                                    int.TryParse(drValue, out intV);
                                    newCell.SetCellValue(intV);
                                    break;
                                case "System.Decimal"://浮点型
                                case "System.Double":
                                    double doubV = 0;
                                    double.TryParse(drValue, out doubV);
                                    if (doubV != 0)
                                        newCell.SetCellValue(doubV);
                                    break;
                                case "System.DBNull"://空值处理
                                    newCell.SetCellValue("");
                                    break;
                                default:
                                    newCell.SetCellValue("");
                                    break;
                            }
                        }
                    }
                    #endregion

                    rowIndex++;
                }
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                ms.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }

        private static void SetHeadStyle(HSSFWorkbook workbook, ISheet sheet, DataTable dtSource)
        {
            IRow headerRow = sheet.CreateRow(0);
            ICellStyle headStyle = workbook.CreateCellStyle();
            //设置对齐方式
            headStyle.Alignment = HorizontalAlignment.Center;
            //设置字体
            IFont font = workbook.CreateFont();
            font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
            font.FontHeightInPoints = 10;
            font.FontName = "宋体";
            headStyle.SetFont(font);
            //设置边框
            headStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            headStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            headStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            headStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            //设置背景色
            headStyle.FillForegroundColor = HSSFColor.LightYellow.Index;
            headStyle.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;

            if (dtSource.TableName == "PurchaseOrders")
            {
                headerRow.CreateCell(0).SetCellValue("编号");
                headerRow.CreateCell(1).SetCellValue("产品线");
                headerRow.CreateCell(2).SetCellValue("客户单号");
                headerRow.CreateCell(3).SetCellValue("下单日期");
                headerRow.CreateCell(4).SetCellValue("采购总数");
                headerRow.CreateCell(5).SetCellValue("采购金额");
                headerRow.CreateCell(6).SetCellValue("实付金额");
                headerRow.CreateCell(7).SetCellValue("折扣金额");
                headerRow.CreateCell(8).SetCellValue("备注");
                //设置列宽度
                for (int index = 0; index < dtSource.Columns.Count; index++)
                {
                    headerRow.GetCell(index).CellStyle = headStyle;
                    SetColumnWidth(index, sheet, headerRow);
                }
            }
            else if (dtSource.TableName == "CustomerOrders")
            {
                headerRow.CreateCell(0).SetCellValue("编号");
                headerRow.CreateCell(1).SetCellValue("客户");
                headerRow.CreateCell(2).SetCellValue("产品线");
                headerRow.CreateCell(3).SetCellValue("客户单号");
                headerRow.CreateCell(4).SetCellValue("下单日期");
                headerRow.CreateCell(5).SetCellValue("联系人");
                headerRow.CreateCell(6).SetCellValue("联系电话");
                headerRow.CreateCell(7).SetCellValue("送货地址");
                headerRow.CreateCell(8).SetCellValue("采购总数");
                headerRow.CreateCell(9).SetCellValue("总金额");
                headerRow.CreateCell(10).SetCellValue("折扣金额");
                headerRow.CreateCell(11).SetCellValue("实付金额");
                headerRow.CreateCell(12).SetCellValue("备注");
               
                //设置列宽度
                for (int index = 0; index < dtSource.Columns.Count; index++)
                {
                    headerRow.GetCell(index).CellStyle = headStyle;
                    SetColumnWidth(index, sheet, headerRow);
                }
            }

        }

        private static ICellStyle SetDataStyle(HSSFWorkbook workbook)
        {
            ICellStyle dataStyle = workbook.CreateCellStyle();
            //设置通用样式
            SetStyle(workbook, dataStyle);
            return dataStyle;
        }

        private static ICellStyle SetDateStyle(HSSFWorkbook workbook)
        {
            ICellStyle dateStyle = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-MM-dd");
            //设置通用样式
            SetStyle(workbook, dateStyle);
            return dateStyle;
        }

        private static void SetStyle(HSSFWorkbook workbook, ICellStyle dateStyle)
        {
            //设置对齐方式
            dateStyle.Alignment = HorizontalAlignment.Center;
            //设置字体
            IFont dataFont = workbook.CreateFont();
            dataFont.FontHeightInPoints = 10;
            dataFont.FontName = "宋体";
            dataFont.Boldweight = 500;
            dateStyle.SetFont(dataFont);
            //dateStyle.WrapText = true; //自动换行
            //设置边框
            dateStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            dateStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            dateStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            dateStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        }

        public static void SetColumnWidth(int columnIndex, ISheet sheet, IRow row, int DefaultAddWidth = 4)
        {
            int columnWidth = sheet.GetColumnWidth(columnIndex) / 256;
            if (columnWidth <= DefaultAddWidth)
            {
                throw new Exception("当前列的宽度必须大于默认的间隙宽度");
            }
            else
            {
                columnWidth = columnWidth - DefaultAddWidth;
            }
            ICell currentCell = row.GetCell(columnIndex);
            int length = Encoding.Default.GetBytes(currentCell.ToString()).Length;
            if (columnWidth < length)
            {
                columnWidth = length;
            }
            sheet.SetColumnWidth(columnIndex, (columnWidth + DefaultAddWidth) * 256);
        }

        /// <summary>
        /// 读取excel 默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <param name="chkEmpty">是否校验列和行的第一个单元格为空，如果开启，由发现为空，将不会读取</param>
        /// <returns></returns>
        public static DataSet ExcelToDataSet(string strFileName, bool chkEmpty = false)
        {
            DataSet ds = new DataSet();

            //HSSFWorkbook hssfworkbook;
            IWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = WorkbookFactory.Create(file); //new HSSFWorkbook(file);
                XSSFFormulaEvaluator e = new XSSFFormulaEvaluator(hssfworkbook);
                for (int i = 0; i < hssfworkbook.NumberOfSheets; i++)
                {
                    DataTable dt = new DataTable();
                    ISheet sheet = hssfworkbook.GetSheetAt(i);
                    System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

                    IRow headerRow = sheet.GetRow(0);
                    if (headerRow == null)
                        continue;

                    int cellCount = headerRow.LastCellNum;

                    for (int j = 0; j < cellCount; j++)
                    {
                        ICell cell = headerRow.GetCell(j);
                        if (chkEmpty)
                        {
                            if (string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                continue;
                            }
                        }
                        dt.Columns.Add(cell.ToString());
                    }

                    bool isRowOK = true;
                    for (int r = (sheet.FirstRowNum + 1); r <= sheet.LastRowNum; r++)
                    {
                        isRowOK = true;
                        IRow row = sheet.GetRow(r);
                        if (row == null)
                            continue;
                        DataRow dataRow = dt.NewRow();

                        int emptyCount = 0;
                        if (row.FirstCellNum < 0) continue;
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (chkEmpty && j == row.FirstCellNum)      //校验第一个单元格 
                            {
                                if (row.GetCell(j) == null || string.IsNullOrEmpty(row.GetCell(j).ToString()))
                                {
                                    isRowOK = false;
                                    break;
                                }
                            }

                            if (row.GetCell(j) != null)
                            {
                                dataRow[j] = CellValue(e, row.GetCell(j));
                            }

                            if (string.IsNullOrEmpty(dataRow[j].ToString()))
                                emptyCount++;
                        }
                        if (isRowOK && (row.FirstCellNum + emptyCount) < cellCount)
                        {
                            dt.Rows.Add(dataRow);
                        }
                    }
                    ds.Tables.Add(dt);
                }
            }
            return ds;
        }

        private static String GetFilePrefix(HttpRequestBase request)
        {
            String url = request.RawUrl;

            int posQuestion = url.LastIndexOf("?");

            if (posQuestion >= 0)
            {
                url = url.Substring(url.LastIndexOf("/") + 1, url.LastIndexOf("?") - url.LastIndexOf("/") - 1);
            }
            else
            {
                url = url.Substring(url.LastIndexOf("/") + 1);
            }

            return url;
        }

        public static String SaveFile(HttpPostedFileBase postedFile, HttpServerUtilityBase server, HttpRequestBase request)
        {
            String fullName = "";
            try
            {
                String fileName = Path.GetFileName(postedFile.FileName);
                String fileExtension = Path.GetExtension(fileName);
                String year = DateTime.Now.Year.ToString();

                DateTime date = DateTime.Now;

                //生成文件名
                String preFix = GetFilePrefix(request);
                String saveName = date.ToString("yyyyMMddHHmmssfffffff");
                String tmpName = preFix + "_" + saveName + fileExtension;

                String path = server.MapPath(@"~/uploadFile");
                fullName = path + @"\" + tmpName;

                postedFile.SaveAs(fullName);
            }
            catch (Exception e)
            {
                throw new Exception("保存上传文件出错！");
            }
            return fullName;
        }

        public static void DeleteFile(String fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        private static object CellValue(IFormulaEvaluator e, ICell cell)
        {
            if (cell == null)
                return null;
            else
            {
                if (cell.CellType == CellType.Boolean)
                    return cell.BooleanCellValue;
                else if (cell.CellType == CellType.Error)
                    return cell.ErrorCellValue;
                else if (cell.CellType == CellType.Numeric)
                    return cell.NumericCellValue;
                else if (cell.CellType == CellType.String)
                    return cell.StringCellValue;
                else if (cell.CellType == CellType.Formula)
                    return e.EvaluateInCell(cell);
                else
                    return cell.ToString();
            }
        }

        #endregion

    }
}
