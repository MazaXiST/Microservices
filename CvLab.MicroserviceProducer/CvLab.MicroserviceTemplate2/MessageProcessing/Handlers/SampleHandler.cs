using System;
using System.Threading.Tasks;

using Rebus.Handlers;

namespace CvLab.MicroserviceTemplate2.MessageProcessing.Handlers
{
    internal class SampleHandler : IHandleMessages<SampleMessage>
    {
        public Task Handle(SampleMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
