using System;
using System.IO;
using System.Threading.Tasks;
using Consul;
using CvLab.Framework.Common.ServiceBus;
using CvLab.Framework.FineSagas.Storages;
using CvLab.Framework.Standard.Common.Configuration;
using CvLab.MicroserviceTemplate1.Config;
using CvLab.MicroserviceTemplate1.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rebus.Config;
using Rebus.ServiceProvider;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.RollingFileSizeLimit.Extensions;
using Serilog.Sinks.RollingFileSizeLimit.Impl;

namespace CvLab.MicroserviceTemplate1
{
    internal static class Program
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="consulAddress">Consul connection configuration</param>
        /// <param name="createDefaultConfig">If <paramref name="configName"/> if not exist, create it at Consul</param>
        /// <param name="configName">Config name for this instance of Platform.Estate.Loudspeaker service</param>
        /// <param name="consulToken">Consul token if one exists</param>
        /// <param name="servicePort">Port at which web services, such as prometheus or rest api, will be available</param>
        /// <returns></returns>
        public static async Task Main(
            string configName,
            Uri consulAddress = null,
            bool createDefaultConfig = false,
            string consulToken = null,
            int servicePort = 51000)
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddConsulJson<ServiceConfiguration>(consulAddress, consulToken, $"CvLab.MicroserviceTemplate1/{configName}", createDefaultConfig);
                    config.AddCommandLine(new[] { $"{nameof(servicePort)}={servicePort}" });
                })
                .UseSerilog((ctx, logCfg) =>
                    logCfg
                        .Enrich.WithProcessName()
                        .Enrich.WithProcessId()
                        .Enrich.WithExceptionDetails()
                        .Enrich.WithMachineName()
                        .Enrich.WithEnvironmentUserName()
                        .MinimumLevel.Is(LogEventLevel.Debug)
                        .WriteTo.Seq(ctx.Configuration.GetSection("Seq").Value, compact: true)
                        .WriteTo.LiterateConsole()
                        .WriteTo.RollingFileSizeLimited(ctx.Configuration.GetSection("LogPath").Value,
                            Path.Combine(ctx.Configuration.GetSection("LogPath").Value, "Archive"),
                            fileSizeLimitBytes: 50 * 1024 * 1024,
                            archiveSizeLimitBytes: 10 * 50 * 1024 * 1024,
                            logFilePrefix: "CvLab.MicroserviceTemplate1",
                            fileCompressor: new DefaultFileCompressor())
                    )
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<ServiceConfiguration>(hostContext.Configuration);

                    services.AddScoped(serviceProvider => hostContext.Configuration.Get<ServiceConfiguration>());

                    services.AddScoped<LiteDbFineSagaStorage>(ResolveFineSagaStorage);

                    services.AutoRegisterHandlersFromAssemblyOf<AppHost>();
                    services.AddRebus((rebusConfigure, serviceProvider) => GetRebus(rebusConfigure, serviceProvider, configName));

                    services.AddSingleton<IConsulClient>(serviceProvider => GetConsul(consulAddress, consulToken, serviceProvider));

                    //TODO: Register new services here
                    services.AddHostedService<AppHost>();
                    services.AddHostedService<RebusHost>();
                    //services.AddHostedService<WebAppHost>();
                });

            builder.Build().RunAsync();

            //var rebus = GetRebus()

            
            //var sender = new Sender();
            //sender.Send("It's my Message!");

        }

        private static RebusConfigurer GetRebus(RebusConfigurer configure, IServiceProvider serviceProvider, string configName)
        {
            var serviceCfg = serviceProvider.GetService<ServiceConfiguration>();

            configure.DeployMessageBus(
                serviceCfg.ServiceBus,
                $"CvLab.MicroserviceTemplate1/{configName}");
            return configure;
        }

        private static ConsulClient GetConsul(Uri consulAddress, string consulToken, IServiceProvider serviceProvider)
        {
            if (consulAddress == null)
            {
                consulAddress = new Uri("http://localhost:8500/");
            }

            return new ConsulClient(cfg =>
            {
                cfg.Address = consulAddress;
                cfg.Token = consulToken;
            });
        }

        private static LiteDbFineSagaStorage ResolveFineSagaStorage(IServiceProvider s)
        {
            var serviceConfiguration = s.GetService<ServiceConfiguration>();
            return new LiteDbFineSagaStorage(serviceConfiguration.FineSagaStorage);
        }
    }
}
