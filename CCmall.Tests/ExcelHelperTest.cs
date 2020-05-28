using CCmall.Common.Configurations;
using CCmall.Common.Helper;
using CCmall.Model.Generator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace CCmall.Tests
{
    public class ExcelHelperTest
    {
        [Fact]
        public void DbHelper_CreateRepository()
        {
            var helper = new ExcelHelper<ExcelTest>();
            var data = new List<ExcelTest>() { new ExcelTest() { id = 11111 } };
            helper.init(data,"data",Excel.OperateType.ALL);

            Assert.True(true);
        }
    }
    public class ExcelTest
    {
        [Excel(name ="id")]
        public int id { get; set; }
    }
}
