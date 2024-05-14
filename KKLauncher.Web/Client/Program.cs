using BlazorComponentBus;
using Blazored.LocalStorage;
using KKLauncher.Web.Client.Authentication;
using KKLauncher.Web.Client.Factories;
using KKLauncher.Web.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Serilog;
using Serilog.Core;

namespace KKLauncher.Web.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();

            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddScoped<AuthenticationStateProvider, KKAuthenticationStateProvider>();

            builder.Services.AddScoped<ComponentBus>();
            builder.Services.AddScoped<IDynamicComponentFactory, DynamicComponentFactory>();

            builder.Services.AddScoped<ILoginService, LoginService>();

            var levelSwitch = new LoggingLevelSwitch();
            var logTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}]  {Message,-120:j}       {NewLine}{Exception}";

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)
                .Enrich.WithProperty("InstanceId", Guid.NewGuid().ToString("n"))
                .WriteTo.BrowserHttp(endpointUrl: builder.HostEnvironment.BaseAddress + "ingest", controlLevelSwitch: levelSwitch)
                .WriteTo.BrowserConsole(outputTemplate: logTemplate)
                .CreateLogger();

            await builder.Build().RunAsync();
        }
    }
}
