using CCmall.Model.Entities;
using CCmall.Repository.Interface;
namespace CCmall.Repository
{
    internal class BaseUserRepository : BaseRepository<BaseUser>, IBaseUserRepository
    {
        public BaseUserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
