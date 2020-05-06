using CCmall.Common.Configurations;
using CCmall.Model.Generator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace CCmall.Tests
{
    public class ClassGenatorTest
    {
        private readonly string _path = @"D:\test";
        private readonly DbHelper _dbHelper;
        private readonly string[] tables = new string[] { "router" };
        /// <summary>
        /// 测试前请删除D:\test文件夹
        /// </summary>
        public ClassGenatorTest()
        {
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            var settings = new Appsettings(basePath);
            _dbHelper = new DbHelper();
        }
        [Fact]
        public void DbHelper_CreateRepository()
        {
            var path = Path.Combine(_path, "Repository");
            _dbHelper.CreateRepository(path, "CCmall", tables);
            Assert.True(Directory.Exists(Path.Combine(path)));
        }

        [Fact]
        public void DbHelper_CreateIRepository()
        {
            var path = Path.Combine(_path, "IRepository");
            _dbHelper.CreateIRepository(path, "CCmall", tables);
            Assert.True(Directory.Exists(Path.Combine(path)));
        }
        [Fact]
        public void DbHelper_CreateEntity()
        {
            var path = Path.Combine(_path, "Entity");
            _dbHelper.CreateEntity(path, "CCmall", tables);
            Assert.True(Directory.Exists(Path.Combine(path)));
        }
    }
}
