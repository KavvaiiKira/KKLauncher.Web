using KKLauncher.Web.Server.Aspects;

namespace KKLauncher.Web.Server.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddScopedWithLogging<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            services.AddScoped<TImplementation>();
            services.AddScoped(provider =>
            {
                var logger = provider.GetRequiredService<ILogger<TImplementation>>();
                var decorated = provider.GetRequiredService<TImplementation>();
                return LoggingAspect<TService>.Create(decorated, logger);
            });
        }
    }
}
