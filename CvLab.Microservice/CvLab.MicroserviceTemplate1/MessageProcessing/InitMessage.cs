using System;
using System.Collections.Generic;
using System.Text;

namespace CvLab.MicroserviceTemplate1.MessageProcessing
{
    public class InitMessage
    {
        public Guid CorrelationId { get; set; } = Guid.NewGuid();
    }
}
