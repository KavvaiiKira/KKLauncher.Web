using KKLauncher.Web.Server.Services;

namespace KKLauncher.Web.Server.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScopedWithLogging<IPCService, PCService>();
            services.AddScopedWithLogging<IAppCollectionService, AppCollectionService>();
            services.AddScopedWithLogging<IAppService, AppService>();
            services.AddScopedWithLogging<ICollectionService, CollectionService>();

            services.AddScopedWithLogging<IAppStartService, AppStartService>();
        }
    }
}
