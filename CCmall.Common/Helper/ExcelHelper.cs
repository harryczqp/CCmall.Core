using Microsoft.Extensions.Logging;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
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
}
