using System;
using Rebus.Bus;

using CvLab.Framework.FineSagas;

namespace CvLab.MicroserviceTemplate1.MessageProcessing.FineSagas
{
    internal class SampleFineSaga : FineSaga<SampleFineSagaData, SampleMessage1>
    {
        private readonly IBus _bus;

        public SampleFineSaga(IFineSagaDataStorage sagaDataStorage, Rebus.Bus.IBus bus) : base(sagaDataStorage, bus)
        {
            _bus = bus ?? throw new System.ArgumentNullException(nameof(bus));
        }

        public override void SetUp(FineSagaConfig<SampleMessage1, SampleFineSagaData> sagaConfig)
        {
            sagaConfig
                .CanInitiateNewSaga(true)
                .SetCorrelation((msg, data) => msg.CorrelationId == data.CorrelationId)
                .SetSagaTimeout(TimeSpan.MaxValue);
        }

        public override bool TryComplete(SampleMessage1 message, SampleFineSagaData sagaData)
        {
            Console.WriteLine("It's fine saga!");
            return false;
           // throw new NotImplementedException();
        }
    }
}
