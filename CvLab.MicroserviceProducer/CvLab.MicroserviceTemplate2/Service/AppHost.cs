using System;
using System.Threading;
using System.Threading.Tasks;
using Anotar.Serilog;
using CvLab.MicroserviceTemplate2.Config;
using Microsoft.Extensions.Hosting;

namespace CvLab.MicroserviceTemplate2.Service
{
    internal class AppHost : BackgroundService
    {
        private readonly ServiceConfiguration _appConfig;

        /// <summary>
        ///     Creates application host from container
        /// </summary>
        /// <param name="bus"></param>
        /// <param name="metricServer"></param>
        /// <param name="appConfig"></param>
        public AppHost(ServiceConfiguration appConfig)
        {
            _appConfig = appConfig ?? throw new ArgumentNullException(nameof(appConfig));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            LogTo.Information("Background task CvLab.MicroserviceTemplate2 started");
        }
    }
}
