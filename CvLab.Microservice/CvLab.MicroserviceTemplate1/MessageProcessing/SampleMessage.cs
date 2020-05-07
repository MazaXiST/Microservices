using System;

namespace CvLab.MicroserviceTemplate1.MessageProcessing
{
    public class SampleMessage1
    {
        public Guid CorrelationId { get; set; } = Guid.NewGuid();

        public string Text { get; set; }
    }
}
