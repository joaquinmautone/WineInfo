using System;
using WineInfo.DAL.DbContexts;

namespace WineInfo.DAL.Infrastructure 
{
    public interface IDatabaseFactory : IDisposable
    {
        WineInfoContext Get();
    }
}
