using KKLauncher.Web.Contracts;
using System.Diagnostics;

namespace KKLauncher.Web.Server.Services
{
    public class AppStartService : IAppStartService
    {
        public AppStartResultDto StartAppWithSteam()
        {
            try
            {
                var startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.FileName = @"C:\Program Files (x86)\Steam\steam.exe";

                startInfo.Arguments = "-applaunch 343780";
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
