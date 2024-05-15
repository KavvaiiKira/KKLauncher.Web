using KKLauncher.Web.Contracts.Apps;

namespace KKLauncher.Web.Server.Services
{
    public interface IAppService
    {
        Task<bool> AddAppAsync(AppDto app);

        Task<IEnumerable<AppViewDto>> GetApplicationsByPCLocalIpAsync(string pcLocalIp);

        Task<AppViewDto?> GetAppViewByIdAsync(Guid appId);

        Task<bool> RemoveAppAsync(Guid appId);
    }
}
