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
    public class MesurementRepository : RepositoryBase<Mesurement>, IMesurementRepository
    {
        public MesurementRepository(IDatabaseFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<IEnumerable<Mesurement>> GetAllAsync()
        {
            var collection = base.DataContext.Mesurements as IQueryable<Mesurement>;
            return await collection.ToListAsync();
        }
    }

    public interface IMesurementRepository : IRepository<Mesurement>
    {
        Task<IEnumerable<Mesurement>> GetAllAsync();
    }
}
