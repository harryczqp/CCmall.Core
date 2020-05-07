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
            var ret = new List<RouterData>();
            var data = Query(f => f.status == (int)CommonEnum.Valid && f.parent == parent).Result;
            if (!data.Any())
            {
                return ret;
            }
            foreach (var item in data)
            {
                var children = GetRouterTree(item.id);
                var model = new RouterData
                {
                    component = item.component.StartsWith("@") ? item.component : $"@{item.component}",
                    meta = new RouterDataMeta { icon = item.icon, title = item.name },
                    name = item.name,
                    path = item.name,
                    id = item.id,
                    children = children
                };
                ret.Add(model);
            }
            return ret;
        }
    }
}
