using KKLauncher.Web.Contracts.ResponseDtos;

namespace KKLauncher.Web.Client.Services
{
    public interface IAppStartService
    {
        Task<AppStartResultDto> StartAppAsync(Guid appId);
    }
}
