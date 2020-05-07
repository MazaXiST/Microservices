using System;
using System.Threading.Tasks;

using Rebus.Handlers;
using Rebus.Bus;

namespace CvLab.MicroserviceTemplate1.MessageProcessing.Handlers
{
    //internal class SampleHandler : IHandleMessages<SampleMessage>
    //{
    //    public Task Handle(SampleMessage message)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    internal class SampleHandler : IHandleMessages<SampleMessage1>
    {
        private readonly IBus _bus;

        public async Task Handle(SampleMessage1 message)
        {
            Console.WriteLine("Message text (from Handler) = {text}",message.Text);
        }
    }
}
