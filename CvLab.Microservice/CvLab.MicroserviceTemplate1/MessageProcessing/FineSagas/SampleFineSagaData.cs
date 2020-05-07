using System;

using CvLab.Framework.FineSagas;

namespace CvLab.MicroserviceTemplate1.MessageProcessing.FineSagas
{
    internal class SampleFineSagaData : IFineSagaData
    {
        public Guid Id { get; set; }
        public int Revision { get; set; }

        public Guid CorrelationId { get; set; }
    }
}
