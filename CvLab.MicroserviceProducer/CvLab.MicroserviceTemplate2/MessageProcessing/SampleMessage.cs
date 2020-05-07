using System;

namespace CvLab.MicroserviceTemplate2.MessageProcessing
{
    public class SampleMessage
    {
        public Guid CorrelationId { get; set; } = Guid.NewGuid();

        public string Text { get; set; }
    }
}
