using KKLauncher.Web.Server.Attributes;

namespace KKLauncher.Web.Server.Utils
{
    public abstract class ServiceStartupBase
    {
        public IConfiguration Configuration { get; set; }

        public ServiceStartupBase()
        {
            if (Attribute.GetCustomAttribute(GetType(), typeof(ConfigurationAttribute)) is not ConfigurationAttribute configurationAttribute)
            {
                throw new Exception("You must set ConfigurationAttribute on you StartUp file");
            }

            Configuration = ConfigurationHelper.BuildConfiguration(configurationAttribute.FileName);
        }

        public abstract WebApplication ConfigureApplication(WebApplicationBuilder builder, Serilog.ILogger logger);
    }
}
