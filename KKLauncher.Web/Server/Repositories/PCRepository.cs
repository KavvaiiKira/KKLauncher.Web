using KKLauncher.DB.Entities;
using KKLauncher.Web.Server.EfCore;
using KKLauncher.Web.Server.Repositories.Base;

namespace KKLauncher.Web.Server.Repositories
{
    public class PCRepository : EntityRepositry<PCEntity, KKLauncherDbContext>, IAsyncRepository<PCEntity>
    {
        public PCRepository(KKLauncherDbContext dbContext) : base(dbContext)
        {

        }
    }
}
