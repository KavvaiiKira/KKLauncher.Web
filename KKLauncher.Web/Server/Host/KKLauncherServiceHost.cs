using KKLauncher.Web.Server.Utils;
using Serilog;
using Serilog.Core;

namespace KKLauncher.Web.Server.Host
{
    public class KKLauncherServiceHost
    {
        private static readonly string _defaultLogLevel = "Debug";
        private static readonly int _defaultLogFileSizeLimitBytes = 1048576;
        private static readonly TimeSpan _defaultRetainedLogFileTimeLimit = TimeSpan.FromSeconds(86400);

        public static void TryRunService(WebApplicationBuilder builder, Func<ServiceStartupBase> createStartup, string serviceName, string logFileName)
        {
            Logger? logger = null;

            try
            {
                logger = LoggerUtils.CreateLogger(_defaultLogLevel, logFileName, _defaultLogFileSizeLimitBytes, _defaultRetainedLogFileTimeLimit);
                logger.Information($"Starting {serviceName} service...");

                var startup = createStartup();
                if (startup == null)
                {
                    throw new NullReferenceException("ServiceStartup can not be null!");
                }

                var sizeLimit = !int.TryParse(startup.Configuration.GetSection("SizeLimit")?.Value, out var sizeLimitConfigValue) ?
                    _defaultLogFileSizeLimitBytes :
                    sizeLimitConfigValue;

                var timeLimit = !int.TryParse(startup.Configuration.GetSection("TimeLimit")?.Value, out var timeLimitConfigValue) ?
                    _defaultRetainedLogFileTimeLimit :
                    TimeSpan.FromSeconds(timeLimitConfigValue);

                var logLevel = startup.Configuration.GetSection("LogLevel").Value;

                logger.Dispose();
                logger = LoggerUtils.CreateLogger(logLevel, logFileName, sizeLimit, timeLimit);

                Log.Logger = logger;

                var app = startup.ConfigureApplication(builder, Log.Logger);

                logger.Information($"Running {serviceName} service...");

                app.Run();
            }
            catch (Exception ex)
            {
                logger?.Fatal(ex, ex.Message);
                logger?.Information($"Can't start {serviceName} service.");

                if (logger != null)
                {
                    logger.Dispose();
                    logger = null;
                }
            }
        }
    }
}
