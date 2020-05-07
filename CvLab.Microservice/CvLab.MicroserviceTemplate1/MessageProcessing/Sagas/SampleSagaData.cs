using System;

using Rebus.Sagas;

namespace CvLab.MicroserviceTemplate1.MessageProcessing.Sagas
{
    internal class SampleSagaData : ISagaData
    {
        public Guid Id { get; set; }
        public int Revision { get; set; }

        public Guid CorrelationId { get; set; }
    }
}
