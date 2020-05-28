using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CCmall.Common.Helper
{
    public class Excel : Attribute
    {
        /// <summary>
        /// 导出到Excel中的名字
        /// </summary>
        /// <returns></returns>
        public string name { get; set; } = string.Empty;
        /// <summary>
        /// 日期格式, 如: yyyy-MM-dd
        /// </summary>
        /// <returns></returns>
        public string dateFormat { get; set; } = string.Empty;
        /// <summary>
        /// 读取内容转表达式 (如: 0=男,1=女,2=未知)
        /// </summary>
        /// <returns></returns>
        public string readConverterExp { get; set; } = string.Empty;


        /// <summary>
        /// 导出类型（0数字 1字符串）
        /// </summary>
        public ColumnType cellType { get; set; } = ColumnType.STRING;

        /// <summary>
        /// 导出时在excel中每个列的高度 单位为字符
        /// </summary>
        public double height { get; set; } = 14;

        /// <summary>
        /// 导出时在excel中每个列的宽 单位为字符
        /// </summary>
        public double width { get; set; } = 16;

        /// <summary>
        /// 文字后缀,如% 90 变成90%
        /// </summary>
        public string suffix { get; set; } = string.Empty;

        /// <summary>
        /// 当值为空时,字段的默认值
        /// </summary>
        public string defaultValue { get; set; } = string.Empty;

        /// <summary>
        /// 提示信息
        /// </summary>
        public string prompt { get; set; } = string.Empty;

        /// <summary>
        /// 设置只能选择不能输入的列内容.
        /// </summary>
        public string[] combo { get; set; } = new string[] { };

        /// <summary>
        /// 是否导出数据,应对需求:有时我们需要导出一份模板,这是标题需要但内容需要用户手工填写.
        /// </summary>
        public bool isExport { get; set; } = true;

        /// <summary>
        /// 另一个类中的属性名称,支持多级获取,以小数点隔开
        /// </summary>
        public string targetAttr { get; set; } = string.Empty;

        /// <summary>
        /// 字段类型（0：导出导入；1：仅导出；2：仅导入）
        /// </summary>
        /// <returns></returns>
        public OperateType operateType { get; set; } = OperateType.ALL;

        public enum OperateType
        {
            ALL,
            EXPORT,
            IMPORT
        }

        public enum ColumnType
        {
            NUMERIC,
            STRING
        }
    }
}
