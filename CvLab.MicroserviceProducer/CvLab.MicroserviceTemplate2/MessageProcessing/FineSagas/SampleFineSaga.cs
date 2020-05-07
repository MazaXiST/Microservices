using System;

using CvLab.Framework.FineSagas;

namespace CvLab.MicroserviceTemplate2.MessageProcessing.FineSagas
{
    internal class SampleFineSaga : FineSaga<SampleFineSagaData, SampleMessage>
    {
        public SampleFineSaga(IFineSagaDataStorage sagaDataStorage, Rebus.Bus.IBus bus) : base(sagaDataStorage, bus)
        {
        }

        public override void SetUp(FineSagaConfig<SampleMessage, SampleFineSagaData> sagaConfig)
        {
            sagaConfig
                .CanInitiateNewSaga(true)
                .SetCorrelation((msg, data) => msg.CorrelationId == data.CorrelationId)
                .SetSagaTimeout(TimeSpan.MaxValue);
        }

        public override bool TryComplete(SampleMessage message, SampleFineSagaData sagaData)
        {
            throw new NotImplementedException();
        }
    }
}
