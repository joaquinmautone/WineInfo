using System;
using System.Collections.Generic;
using System.Text;
using WineInfo.DAL.Infrastructure;
using WineInfo.Entities;

namespace WineInfo.DAL.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDatabaseFactory dbFactory) : base(dbFactory)
        {

        }
    }

    public interface IUserRepository : IRepository<User>
    {

    }
}
