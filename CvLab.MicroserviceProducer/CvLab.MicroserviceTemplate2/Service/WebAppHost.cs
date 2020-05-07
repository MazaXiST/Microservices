using System;
using System.Threading;
using System.Threading.Tasks;
using Anotar.Serilog;
using CvLab.MicroserviceTemplate2.Controllers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Prometheus;

namespace CvLab.MicroserviceTemplate2.Service
{
    internal class WebAppHost : IHostedService
    {
        private IWebHost _webHost;
        private IConfiguration _webAppInnerConfig;
        private readonly ILoggerFactory _loggerFactory;

        public WebAppHost(IConfiguration webAppInnerConfig, ILoggerFactory loggerFactory)
        {
            _webAppInnerConfig = webAppInnerConfig ?? throw new ArgumentNullException(nameof(webAppInnerConfig));
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var webHostBuilder = WebHost.CreateDefaultBuilder();

            var servicePort = _webAppInnerConfig.GetValue<int>("servicePort");
            if (servicePort == 0)
                throw new InvalidOperationException("Arrgument servicePort is not specyfied at config");

            _webHost = webHostBuilder
                .UseConfiguration(_webAppInnerConfig)
                .ConfigureServices((ctx, services) =>
                {
                    services.AddTransient<SampleController>();

                    services.AddMvcCore()
                        .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                        .AddApiExplorer()
                        .AddJsonFormatters();

                    services.AddSwaggerGen(c =>
                    {
                        c.SwaggerDoc("v1", new OpenApiInfo
                        {
                            Title = "CvLab.MicroserviceTemplate2 API",
                            Version = "v1",
                            Description = "CvLab.MicroserviceTemplate2"
                        });
                    });

                    services.AddSingleton(_loggerFactory);
                    services.AddLogging();
                })
                .Configure(app =>
                {
                    app.UseMetricServer();
                    app.UseSwagger();
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CvLab.MicroserviceTemplate2 API V1");
                    });

                    app.UseMvc();
                })
                .UseKestrel(opt => opt.ListenAnyIP(servicePort))
                .Build();

            LogTo.Information("WebHost CvLab.MicroserviceTemplate2 starting at {WebHostPort}", servicePort);
            await _webHost.StartAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _webHost
                .StopAsync()
                .ConfigureAwait(false);
        }
    }
}
