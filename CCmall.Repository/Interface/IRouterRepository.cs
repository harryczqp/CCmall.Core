using CCmall.Model.Entities;
using CCmall.Model.Models;
using System.Collections.Generic;

namespace CCmall.Repository.Interface
{
    public interface IRouterRepository : IBaseRepository<Router>
    {
        List<RouterData> GetRouterTree(int parent);
    }
}
