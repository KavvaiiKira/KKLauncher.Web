using KKLauncher.Web.Contracts.Apps;

namespace KKLauncher.Web.Client.Services
{
    public interface IAppService
    {
        Task<bool> AddAppAsync(AppDto appDto);
    }
}
