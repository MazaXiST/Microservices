using System;
using System.Threading;
using System.Threading.Tasks;
using CvLab.MicroserviceTemplate1.MessageProcessing;
using Microsoft.Extensions.Hosting;
using Rebus.Activation;
using Rebus.Bus;
using Rebus.Config;

namespace CvLab.MicroserviceTemplate1.Service
{
    internal class RebusHost : IHostedService
    {
        BuiltinHandlerActivator adapter = new BuiltinHandlerActivator();
        private readonly IBus _bus;

        public RebusHost(IBus bus)
        {
            _bus = adapter.Bus;
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        public void Receive()
        {
            adapter.Handle<SampleMessage1>(async method =>
            {
                Console.WriteLine("Processing message {0}", method.Text);
            });

            Configure.With(adapter)
                .Logging(l => l.ColoredConsole(Rebus.Logging.LogLevel.Warn))
                .Transport(t => t.UseRabbitMq("amqp://user:12345@10.15.0.15", "consumer"))
                .Options(o => o.SetMaxParallelism(5))
                .Start();

            adapter.Bus.Subscribe<SampleMessage1>().Wait();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //await _bus.Subscribe<SampleMessage1>();
            Receive();

            Console.WriteLine("<RebusHost started!>");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            //await _bus.Unsubscribe<SampleMessage1>();

            Console.WriteLine("<RebusHost stopped!>");
        }
    }
}
