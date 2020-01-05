using CCmall.Model;
using CCmall.Model.Models;
using CCmall.Repository.Interface;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CCmall.Repository
{
    public class BaseUserRepository : BaseRepository<base_user>, IBaseUserRepository
    {
        public BaseUserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
