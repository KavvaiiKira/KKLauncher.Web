using KKLauncher.DB.Entities;
using KKLauncher.Web.Server.EfCore;
using KKLauncher.Web.Server.Repositories.Base;

namespace KKLauncher.Web.Server.Repositories
{
    public class AppCollectionRepository : EntityRepositry<AppCollectionEntity, KKLauncherDbContext>, IAsyncRepository<AppCollectionEntity>
    {
        public AppCollectionRepository(KKLauncherDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
