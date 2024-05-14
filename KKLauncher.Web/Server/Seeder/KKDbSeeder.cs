using KKLauncher.DB.Entities;
using KKLauncher.Web.Server.EfCore;
using Microsoft.EntityFrameworkCore;

namespace KKLauncher.Web.Server.Seeder
{
    public class KKDbSeeder
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            var scopeFactory = applicationBuilder.ApplicationServices.GetService<IServiceScopeFactory>();
            if (scopeFactory == null)
            {
                throw new ArgumentNullException("Can't get IServiceScopeFactory from Application Services", nameof(applicationBuilder));
            }

            using var serviceScope = scopeFactory.CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<KKLauncherDbContext>();

            context.Database.Migrate();
        }
    }
}
