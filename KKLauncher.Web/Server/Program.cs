using KKLauncher.Web.Server.Host;
using System.Reflection;

namespace KKLauncher.Web.Server
{
    public class Program
    {
        private const string _serviceName = "KKLauncherServer";
        private const string _logFileName = "KKLauncherServer";

        public static void Main(string[] args)
        {
            var dirLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (dirLocation != null)
            {
                Directory.SetCurrentDirectory(dirLocation);
            }

            KKLauncherServiceHost.TryRunService(WebApplication.CreateBuilder(args),
                () => new ServiceStartup(),
                _serviceName,
                _logFileName);
        }
    }
}
