using Microsoft.EntityFrameworkCore;
using System;
using WineInfo.DAL.Persistence;
using WineInfo.Entities;

namespace WineInfo.DAL.DbContexts
{
    public class WineInfoContext : DbContext
    {
        public DbSet<Mesurement> Mesurements { get; set; }
        public DbSet<User> Users { get; set; }

        public WineInfoContext() : base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=WineInfoDB");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new MesurementConfiguration().Configure(modelBuilder.Entity<Mesurement>());
            new UserConfiguration().Configure(modelBuilder.Entity<User>());
        }
    }
}
