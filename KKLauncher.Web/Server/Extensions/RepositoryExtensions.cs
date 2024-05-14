using KKLauncher.DB.Entities;
using KKLauncher.Web.Server.Repositories;
using KKLauncher.Web.Server.Repositories.Base;

namespace KKLauncher.Web.Server.Extensions
{
    public static class RepositoryExtensions
    {
        public static void AddRespositories(this IServiceCollection services)
        {
            services.AddScopedWithLogging<IAsyncRepository<PCEntity>, PCRepository>();
            services.AddScopedWithLogging<IAsyncRepository<AppCollectionEntity>, AppCollectionRepository>();
            services.AddScopedWithLogging<IAsyncRepository<AppEntity>, AppRepository>();
            services.AddScopedWithLogging<IAsyncRepository<CollectionEntity>, CollectionRepository>();
        }
    }
}
