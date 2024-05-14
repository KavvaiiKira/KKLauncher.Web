using KKLauncher.Web.Server.Attributes;
using KKLauncher.Web.Server.EfCore;
using KKLauncher.Web.Server.Extensions;
using KKLauncher.Web.Server.Filters;
using KKLauncher.Web.Server.Seeder;
using KKLauncher.Web.Server.Utils;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using Serilog;
using Serilog.Events;
using System.Reflection;
using System.Text.Json.Serialization;

namespace KKLauncher.Web.Server
{
    [Configuration("kk-launcher")]
    public class ServiceStartup : ServiceStartupBase
    {
        public override WebApplication ConfigureApplication(WebApplicationBuilder builder, Serilog.ILogger logger)
        {
            builder = ConfigureBuilder(builder);

            builder.Configuration.AddConfiguration(Configuration);

            builder.Logging.AddSerilog(logger);
            builder.Host.UseSerilog();
            builder.Host.UseWindowsService();

            builder.Services.AddRazorPages();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddResponseCaching();

            ConfigureServices(builder.Services);

            var app = builder.Build();

            app.UseResponseCaching();
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger KKLauncher Server API");
            });

            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseCors("CorsPolicy");
            app.UseSerilogIngestion();
            app.UseSerilogRequestLogging(s => s.GetLevel = DefaultGetLevel);
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapRazorPages();
            app.MapControllers();
            app.Map("api/{**slug}", HandleApiFallback);
            app.MapFallbackToFile("{**slug}", "index.html");

            KKDbSeeder.Seed(app);

            return app;
        }

        private IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptinFilter));
            })
                .AddControllersAsServices();

            var connectionString = Configuration.GetValue<string>("ConnectionString");

            services.AddCors(options =>
            {
                options.AddPolicy(
                    "CorsPolicy",
                    builder =>
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddSignalR();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptinFilter));
                options.EnableEndpointRouting = false;
            })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = GetType().Assembly.GetName().Version?.ToString() ?? string.Empty,
                    Title = "Kavvaii Kira Launcher Web Server"
                });
            });

            services.AddRespositories();
            services.AddServices();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var migrationAssembly = typeof(ServiceStartup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<KKLauncherDbContext>(options =>
            {
                options.UseNpgsql(
                    connectionString,
                    option =>
                    {
                        option.MigrationsAssembly(migrationAssembly);
                        option.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                    });
            });

            return services.BuildServiceProvider();
        }

        private Task HandleApiFallback(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = 404;
            return Task.CompletedTask;
        }

        private LogEventLevel DefaultGetLevel(HttpContext httpContext, double _, Exception? ex) =>
            ex != null ?
                LogEventLevel.Error :
                httpContext.Response.StatusCode > 499 ?
                    LogEventLevel.Error :
                    LogEventLevel.Verbose;

        private WebApplicationBuilder ConfigureBuilder(WebApplicationBuilder builder)
        {
            var ports = Configuration.GetSection("Ports").Get<Ports>()!;

            builder.WebHost.ConfigureKestrel(kestrel =>
            {
                kestrel.ListenAnyIP(ports.Https, l => l.UseHttps());
                kestrel.ListenAnyIP(ports.Http);
            });

            return builder;
        }

        private class Ports
        {
            public int Http { get; set; }

            public int Https { get; set; }
        }
    }
}
