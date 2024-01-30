using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WineInfo.DAL.Infrastructure;
using WineInfo.Entities;

namespace WineInfo.DAL.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDatabaseFactory dbFactory) : base(dbFactory)
        {
            
        }

        public async Task<User> AddAsync(User entity)
        {
            await base.DataContext.AddAsync(entity);

            return entity;
        }

        public async Task<User> FindByUserNameAsync(string userName)
        {
            var collection = base.DataContext.Users as IQueryable<User>;
            return await collection.Where(x => x.UserName == userName).FirstOrDefaultAsync();
        }
    }

    public interface IUserRepository : IRepository<User>
    {
        Task<User> AddAsync(User user);
        Task<User> FindByUserNameAsync(string userName);
    }
}
