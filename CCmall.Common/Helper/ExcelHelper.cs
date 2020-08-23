using Microsoft.Extensions.Logging;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using static CCmall.Common.Helper.Excel;

namespace CCmall.Common.Helper
{
    public class ExcelHelper<T>
    {
        //Excel sheet最大行数，默认65536
        public static int sheetSize = 65536;
        //工作表名称
        private string sheetName;
        //导出类型（EXPORT:导出数据；IMPORT：导入模板）
        private OperateType type;
        //工作薄对象
        private IWorkbook wb;
        //工作表对象
        private ISheet sheet;
        //样式列表
        private Dictionary<string, ICellStyle> styles;
        //导入导出数据列表
        private List<T> list;
        //注解列表
        private List<Object[]> fields;
        public void init(List<T> list, String sheetName, OperateType type)
        {
            if (list == null)
            {
                list = new List<T>();
            }
            this.list = list;
            this.sheetName = sheetName;
            this.type = type;
            createExcelField();
            createWorkbook();
        }
        /// <summary>
        /// 得到所有定义字段
        /// </summary>
        private void createExcelField()
        {
            this.fields = new List<Object[]>();
            List<T> tempFields = new List<T>();
            var customAttrs = typeof(T).GetProperties().Where(f => f.CustomAttributes.Any(f => f.AttributeType == typeof(Excel))).Select(s => s.GetCustomAttribute<Excel>());
        }
        /// <summary>
        /// 创建一个工作簿
        /// </summary>
        private void createWorkbook()
        {
            this.wb = new HSSFWorkbook();
        }

        /// <summary>
        /// 放到字段集合中
        /// </summary>
        /// <param name="field"></param>
        /// <param name="attr"></param>
        private void putToField(Object field, Excel attr)
        {
            if (attr != null && (attr.operateType == OperateType.ALL || attr.operateType == type))
            {
                this.fields.Add(new Object[] { field, attr });
            }
        }
    }


    public class CommonExcelHelper
    {
        public List<string> ReadLastLine(string file)
        {
            var ret = new List<string>();
            using (var fs = File.OpenRead(file))
            {
                IWorkbook workbook;
                if (file.IndexOf(".xlsx") > 0)
                    workbook = new XSSFWorkbook(fs);
                // 2003版本
                else
                    workbook = new HSSFWorkbook(fs);

                if (workbook == null)
                {
                    throw new Exception("workbook is null");
                }
                var sheet = workbook.GetSheetAt(0);
                if (sheet == null)
                {
                    throw new Exception("sheet is null");
                }
                int rowCount = sheet.LastRowNum;//总行数
                if (rowCount == 0)
                {
                    throw new Exception("rowCount is 0");
                }
                var lastdata = sheet.GetRow(rowCount);
                var cellCount = lastdata.LastCellNum;
                for (int i = 0; i < cellCount - 1; i++)
                {
                    var value = string.Empty;
                    switch (lastdata.GetCell(i).CellType)
                    {
                        case CellType.Unknown:
                            break;
                        case CellType.Numeric:
                            value = lastdata.GetCell(i).NumericCellValue.ToString();
                            break;
                        case CellType.String:
                            value = lastdata.GetCell(i).StringCellValue;
                            break;
                        case CellType.Boolean:
                            value = lastdata.GetCell(i).BooleanCellValue?"true":"false";
                            break;
                        default:
                            break;
                    }
                    ret.Add(value);
                }
            }
            return ret;
        }
    }
}
