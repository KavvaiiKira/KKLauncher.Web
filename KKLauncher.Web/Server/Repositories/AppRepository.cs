using KKLauncher.DB.Entities;
using KKLauncher.Web.Server.EfCore;
using KKLauncher.Web.Server.Repositories.Base;

namespace KKLauncher.Web.Server.Repositories
{
    public class AppRepository : EntityRepositry<AppEntity, KKLauncherDbContext>, IAsyncRepository<AppEntity>
    {
        public AppRepository(KKLauncherDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
