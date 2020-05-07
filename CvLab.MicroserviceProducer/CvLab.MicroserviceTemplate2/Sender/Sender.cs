using System;
using System.Collections.Generic;
using System.Text;
using Rebus.Activation;
using Rebus.Bus;
using Rebus.Config;
using CvLab.MicroserviceTemplate2.MessageProcessing;
using CvLab.MicroserviceTemplate1.MessageProcessing;

namespace CvLab.MicroserviceTemplate2.SenderClass
{
    public interface ISend
    {
        void Send(string message);
    }

    public class Sender : ISend
    {
        private BuiltinHandlerActivator adapter = new BuiltinHandlerActivator();

        IBus _bus;

        public Sender(IBus bus)
        {
            _bus = bus;

            Configure.With(adapter)
                .Logging(l => l.ColoredConsole(Rebus.Logging.LogLevel.Warn))
                .Transport(t => t.UseRabbitMqAsOneWayClient("amqp://user:12345@10.15.0.15"))
                .Start();
        }

        public void Send(string message)
        {
            Publish(message, _bus);// adapter.Bus);
        }

        void Publish(string mes, IBus bus)
        {
            Console.WriteLine("Publishing {0}", mes);

            bus.Publish(new SampleMessage1 { Text = mes });
        }
    }
}
