using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace KKLauncher.Web.Server.Utils
{
    public class LoggerUtils
    {
        private static readonly string _outputTemplate = "{Timestamp:dd.MM.yyyy HH:mm:ss:fff} {Level:u1} [{SourceContext}] {Message:l}{Exception}{NewLine}";

        public static Logger CreateLogger(string logLevel, string logName, int logFileSizeLimit, TimeSpan logFileTimeLimit)
        {
            var level = LogEventLevel.Debug;

            switch(logLevel)
            {
                case "Verbose":
                    level = LogEventLevel.Verbose;
                    break;
                case "Debug":
                    level = LogEventLevel.Debug;
                    break;
                case "Info":
                    level = LogEventLevel.Information;
                    break;
                case "Warning":
                    level = LogEventLevel.Warning;
                    break;
                case "Error":
                    level = LogEventLevel.Error;
                    break;
            }

            var logPath = Path.Combine(".", "logs", $"{logName}-.log");

            return new LoggerConfiguration()
                .MinimumLevel.Is(level)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetcore", LogEventLevel.Error)
                .Enrich.FromLogContext()
                .WriteTo.Console(
                    outputTemplate: _outputTemplate,
                    theme: AnsiConsoleTheme.Literate)
                .WriteTo.File(
                    logPath,
                    outputTemplate: _outputTemplate,
                    shared: true,
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 30,
                    retainedFileTimeLimit: logFileTimeLimit,
                    rollOnFileSizeLimit: true,
                    fileSizeLimitBytes: logFileSizeLimit)
                .CreateLogger();
        }
    }
}
