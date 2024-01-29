using WineInfo.DAL.DbContexts;

namespace WineInfo.DAL.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private WineInfoContext dbContext;
        private readonly IDatabaseFactory dbFactory;

        protected WineInfoContext DbContext
        {
            get
            {
                return dbContext ?? dbFactory.Get();
            }
        }

        public UnitOfWork(IDatabaseFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }
    }
}
