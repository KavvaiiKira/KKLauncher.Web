using Serilog;
using System.Collections;

namespace KKLauncher.Web.Server.Utils
{
    public class ConfigurationHelper
    {
        public static IConfigurationRoot BuildConfiguration(string configName)
        {
            var configPath = Path.GetFullPath("config");

            var builder = new ConfigurationBuilder()
                .SetBasePath(configPath)
                .AddJsonFile(
                    configName,
                    optional: false,
                    reloadOnChange: false);

            var jsonConfig = builder.Build();
            var inMemmory = new Dictionary<string, string>();

            foreach (DictionaryEntry environmentVariable in Environment.GetEnvironmentVariables())
            {
                var key = environmentVariable.Key.ToString();
                if (jsonConfig.GetValue<string>(key) != null)
                {
                    Log.Logger.Warning($"Variable \"{key}\" was declared in both \"{configName}\" and variables.environment: JSON confog value will be used.");
                    continue;
                }

                inMemmory.Add(key, environmentVariable.Value.ToString());
            }

            builder.AddInMemoryCollection(inMemmory);

            return builder.Build();
        }
    }
}
