using KKLauncher.DB.Entities;
using KKLauncher.Web.Contracts.ResponseDtos;
using KKLauncher.Web.Server.Repositories.Base;
using System.Diagnostics;

namespace KKLauncher.Web.Server.Services
{
    public class AppStartService : IAppStartService
    {
        private readonly IAsyncRepository<AppEntity> _appRepository;
        private readonly IAsyncRepository<PCEntity> _pcRepository;

        public AppStartService(IAsyncRepository<AppEntity> appRepository, IAsyncRepository<PCEntity> pcRepository)
        {
            _appRepository = appRepository;
            _pcRepository = pcRepository;
        }

        public async Task<AppStartResultDto> StartAppAsync(Guid appId)
        {
            try
            {
                var app = await _appRepository.FirstOrDefaultAsync(a => a.Id == appId);
                if (app == null)
                {
                    throw new ArgumentNullException($"Application with ID: {appId} not found!");
                }

                var pc = await _pcRepository.FirstOrDefaultAsync(p => p.Id == app.PCId);
                if (pc == null)
                {
                    throw new ArgumentNullException($"PC with ID: {app.PCId} not found!");
                }

                var startInfo = new ProcessStartInfo();

                if (string.IsNullOrEmpty(app.SteamId))
                {
                    startInfo.FileName = app.Path;
                }
                else
                {
                    startInfo.CreateNoWindow = false;
                    startInfo.UseShellExecute = false;
                    startInfo.FileName = pc.SteamPath;

                    startInfo.Arguments = $"-applaunch {app.SteamId}";
                }

                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                return new AppStartResultDto($"App not started! Error message: {ex.Message}");
            }

            return new AppStartResultDto(true);
        }
    }
}
