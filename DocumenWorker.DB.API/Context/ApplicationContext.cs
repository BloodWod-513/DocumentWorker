using DocumenWorker.DB.API.Context.Interfaces;
using DocumenWorker.DB.API.Domains;
using DocumenWorker.DB.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace DocumenWorker.DB.API.Context
{
    public class ApplicationContext : DbContext, IDBContext
    {
        public DbSet<WordInfoDomain> WordInfos { get; set; }
        public DbSet<WordInfoTempDomain> WordInfoTemps { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WordInfoTempDomain>()
                .Property(p => p.IsNew)
                .HasDefaultValue(true);
        }
    }
}
