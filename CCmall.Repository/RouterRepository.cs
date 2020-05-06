using CCmall.Common.Enum;
using CCmall.Model.Entities;
using CCmall.Model.Models;
using CCmall.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace CCmall.Repository
{
    internal class RouterRepository : BaseRepository<Router>, IRouterRepository
    {
        public RouterRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<RouterData> GetRouterTree(int parent)
        {
            var data = Query(f => f.status == (int)CommonEnum.Valid && f.parent == parent).Result.Select(s => new RouterData
            {
                componment=s.componment,
                meta=new RouterDataMeta { icon=s.icon,title=s.name},
                name=s.name,
                path=s.name,
                children= GetRouterTree(s.parent)
            });
            if (!data.Any())
            {
                return new List<RouterData>();
            }
            return data.ToList();
        }
    }
}
