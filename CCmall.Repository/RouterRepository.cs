using CCmall.Model.Entities;
using CCmall.Repository.Interface;
namespace CCmall.Repository
{
    internal class RouterRepository : BaseRepository<Router>, IRouterRepository
    {
        public RouterRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
