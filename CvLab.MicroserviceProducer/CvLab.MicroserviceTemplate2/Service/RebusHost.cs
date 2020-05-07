using System;
using System.Threading;
using System.Threading.Tasks;
using CvLab.MicroserviceTemplate2.MessageProcessing;
using Microsoft.Extensions.Hosting;
using Rebus.Bus;
using Rebus.Activation;
using Rebus.Config;

using CvLab.MicroserviceTemplate2.SenderClass;

namespace CvLab.MicroserviceTemplate2.Service
{
    internal class RebusHost : IHostedService
    {
        BuiltinHandlerActivator adapter = new BuiltinHandlerActivator();
        private readonly IBus _bus;

        public RebusHost(IBus bus)
        {
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        void Send()
        {
            //        Configure.With(adapter)
            //.Logging(l => l.ColoredConsole(Rebus.Logging.LogLevel.Warn))
            //.Transport(t => t.UseRabbitMq("amqp://user:12345@10.15.0.15", "consumer"))
            //.Options(o => o.SetMaxParallelism(5))
            //.Start();

            var sender = new Sender(_bus);
            sender.Send("IT'S SOME MESSAGE!");
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Push any button to SEND!");
            Console.ReadKey();
            Send();
            Console.WriteLine("Push any button to QUIT!");
            Console.ReadKey();
            //await _bus.Subscribe<SampleMessage>();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            //await _bus.Unsubscribe<SampleMessage>();
        }
    }
}
