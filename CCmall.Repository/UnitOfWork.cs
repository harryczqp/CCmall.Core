using CCmall.Repository.Interface;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using DotNetCore.CAP;

namespace CCmall.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISqlSugarClient _sqlSugarClient;
        private readonly ICapPublisher _capPublisher;

        public UnitOfWork(ISqlSugarClient sqlSugarClient,ICapPublisher capPublisher)
        {
            _sqlSugarClient = sqlSugarClient;
            _capPublisher = capPublisher;
        }

        /// <summary>
        /// 获取DB，保证唯一性
        /// </summary>
        /// <returns></returns>
        public SqlSugarClient GetDbClient()
        {
            // 必须要as，后边会用到切换数据库操作
            return _sqlSugarClient as SqlSugarClient;
        }

        public void BeginTran()
        {
            GetDbClient().BeginTran();
        }

        public void CommitTran()
        {
            try
            {
                GetDbClient().CommitTran(); 
            }
            catch (Exception ex)
            {
                GetDbClient().RollbackTran();
                throw ex;
            }
        }

        public void RollbackTran()
        {
            GetDbClient().RollbackTran();
        }

        public void Cap()
        {
            //GetDbClient().Ado.BeginTran
        }
    }
}
