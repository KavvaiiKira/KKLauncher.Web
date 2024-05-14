using KKLauncher.DB.Entities;
using KKLauncher.Web.Server.EfCore;
using KKLauncher.Web.Server.Repositories.Base;

namespace KKLauncher.Web.Server.Repositories
{
    public class CollectionRepository : EntityRepositry<CollectionEntity, KKLauncherDbContext>, IAsyncRepository<CollectionEntity>
    {
        public CollectionRepository(KKLauncherDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
