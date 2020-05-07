using System;
using System.Threading.Tasks;

using Rebus.Sagas;

namespace CvLab.MicroserviceTemplate2.MessageProcessing.Sagas
{
    internal class SampleSaga : Saga<SampleSagaData>,
        IAmInitiatedBy<SampleMessage>
    {
        public Task Handle(SampleMessage message)
        {
            throw new NotImplementedException();
        }

        protected override void CorrelateMessages(ICorrelationConfig<SampleSagaData> config)
        {
            config.Correlate<SampleMessage>(m => m.CorrelationId, d => d.CorrelationId);
        }
    }
}
