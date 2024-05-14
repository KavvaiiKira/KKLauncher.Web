using KKLauncher.DB.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KKLauncher.Web.Server.EfCore
{
    public class KKLauncherDbContext : DbContext
    {
        public KKLauncherDbContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<PCEntity> PCs { get; set; }

        public virtual DbSet<AppEntity> Applications { get; set; }

        public virtual DbSet<CollectionEntity> Collections { get; set; }

        public virtual DbSet<AppCollectionEntity> AppCollection { get; set; }

        public virtual DbSet<WhitelistEntity> Whitelists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
