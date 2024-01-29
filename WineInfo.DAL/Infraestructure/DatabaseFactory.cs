using WineInfo.DAL.DbContexts;

namespace WineInfo.DAL.Infrastructure 
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private WineInfoContext dataContext;

        public WineInfoContext Get()
        {
            return dataContext ?? (dataContext = new WineInfoContext());
        }

        protected override void DisposeCore()
        {
            if (dataContext != null) dataContext.Dispose();
        }
    }
}
