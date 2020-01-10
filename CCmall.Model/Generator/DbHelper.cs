using System;
using System.Collections.Generic;
using System.Text;
using CCmall.Common.Configurations;
using SqlSugar;
using System.Linq;

namespace CCmall.Model.Generator
{
    public class DbHelper
    {
        private readonly SqlSugarClient _db;
        private const string _nextline = "\r\n";

        public DbHelper()
        {
            var listConfig = new List<ConnectionConfig>();
            var configSettings = Appsettings.BaseDB;
            foreach (var setting in configSettings)
            {
                listConfig.Add(new ConnectionConfig
                {
                    ConfigId = setting.ConnId,
                    ConnectionString = setting.Connection,
                    DbType = (DbType)setting.DbType,
                    IsAutoCloseConnection = true,
                    IsShardSameThread = false,
                    MoreSettings = new ConnMoreSettings()
                    {
                        IsAutoRemoveDataCache = true
                    }
                });
            }
            _db = new SqlSugarClient(listConfig);
        }

        public void CreateEntity(string strPath, string strNamespace, string[] listTable, string strParent = "", bool blnSerializable = false)
        {
            var IdbFirst = _db.DbFirst;
            if (listTable != null && listTable.Any())
            {
                IdbFirst = IdbFirst.Where(listTable);
            }
            IdbFirst.SettingClassTemplate(old =>
            {
                StringBuilder sBuilder = new StringBuilder();
                sBuilder.Append($"namespace {strNamespace }.Model.Entities{_nextline}");
                sBuilder.Append(@"{" + _nextline);
                sBuilder.Append(@"    {ClassDescription}{SugarTable}");
                if (blnSerializable)
                {
                    sBuilder.Append(@"[Serializable]");
                }
                sBuilder.Append(_nextline);
                sBuilder.Append(@"    public class {ClassName}");
                //是否存在其他继承关系
                if (!string.IsNullOrEmpty(strParent))
                {
                    sBuilder.Append($": {strParent}");
                }
                sBuilder.Append(_nextline);
                sBuilder.Append(@"    {" + _nextline);
                sBuilder.Append(@"        public {ClassName}()" + _nextline);
                sBuilder.Append(@"        {" + _nextline);
                sBuilder.Append(@"        }" + _nextline);
                sBuilder.Append(@"        {PropertyName}" + _nextline);
                sBuilder.Append(@"    }" + _nextline);
                sBuilder.Append(@"}" + _nextline);
                return sBuilder.ToString();
            })
                //        .SettingPropertyTemplate(old => old = @"
                //{SugarColumn}
                //public {PropertyType} {PropertyName} { get; set; }

                //    ")
                .SettingPropertyTemplate(old =>
                {
                    StringBuilder sBuilder = new StringBuilder();
                    sBuilder.Append(_nextline);
                    sBuilder.Append(@"        {SugarColumn}" + _nextline);
                    sBuilder.Append(@"        public {PropertyType} {PropertyName} { get; set; }" + _nextline);
                    sBuilder.Append(_nextline);
                    return sBuilder.ToString();
                })
                .CreateClassFile(strPath, strNamespace);
        }

        public void CreateRepository(string strPath, string strNamespace, string[] listTable, string strParent = "")
        {
            var IdbFirst = _db.DbFirst;
            if (listTable != null && listTable.Any())
            {
                IdbFirst = IdbFirst.Where(listTable);
            }
            IdbFirst.SettingClassTemplate(old =>
            {
                StringBuilder sBuilder = new StringBuilder();
                sBuilder.Append($"using CCmall.Model.Entities;{_nextline}");
                sBuilder.Append($"using CCmall.Repository.Interface;{_nextline}");
                sBuilder.Append($"namespace {strNamespace }.Repository{_nextline}");
                sBuilder.Append(@"{" + _nextline);
                sBuilder.Append(@"    internal class {ClassName}Repository : BaseRepository<{ClassName}>, I{ClassName}Repository");
                //是否存在其他继承关系
                if (!string.IsNullOrEmpty(strParent))
                {
                    sBuilder.Append($": {strParent}");
                }
                sBuilder.Append(_nextline);
                sBuilder.Append(@"    {" + _nextline);
                sBuilder.Append(@"        public {ClassName}Repository(IUnitOfWork unitOfWork) : base(unitOfWork)" + _nextline);
                sBuilder.Append(@"        {" + _nextline);
                sBuilder.Append(@"        }" + _nextline);
                sBuilder.Append(@"    }" + _nextline);
                sBuilder.Append(@"}" + _nextline);
                return sBuilder.ToString();
            }).CreateClassFile(strPath, strNamespace);
        }

        public void CreateIRepository(string strPath, string strNamespace, string[] listTable, string strParent = "")
        {
            var IdbFirst = _db.DbFirst;
            if (listTable != null && listTable.Any())
            {
                IdbFirst = IdbFirst.Where(listTable);
            }
            IdbFirst.SettingClassTemplate(old =>
            {
                StringBuilder sBuilder = new StringBuilder();
                sBuilder.Append($"using CCmall.Model.Entities;{_nextline}");
                sBuilder.Append($"namespace {strNamespace }.Repository.Interface{_nextline}");
                sBuilder.Append(@"{" + _nextline);
                sBuilder.Append(@"    public interface I{ClassName}Repository : IBaseRepository<{ClassName}>");
                //是否存在其他继承关系
                if (!string.IsNullOrEmpty(strParent))
                {
                    sBuilder.Append($": {strParent}");
                }
                sBuilder.Append(_nextline);
                sBuilder.Append(@"    {" + _nextline);
                sBuilder.Append(@"    }" + _nextline);
                sBuilder.Append(@"}" + _nextline);
                return sBuilder.ToString();
            }).CreateClassFile(strPath, strNamespace);
        }
    }
}
