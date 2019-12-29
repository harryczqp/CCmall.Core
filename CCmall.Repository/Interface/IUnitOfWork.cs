using SqlSugar;

namespace CCmall.Repository.Interface
{
    public interface IUnitOfWork
    {
        SqlSugarClient GetDbClient();
        void BeginTran();
        void CommitTran();
        void RollbackTran();
    }
}