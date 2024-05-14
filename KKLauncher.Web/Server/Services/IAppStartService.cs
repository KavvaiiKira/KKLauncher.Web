using KKLauncher.Web.Contracts;

namespace KKLauncher.Web.Server.Services
{
    public interface IAppStartService
    {
        AppStartResultDto StartAppWithSteam();
    }
}
