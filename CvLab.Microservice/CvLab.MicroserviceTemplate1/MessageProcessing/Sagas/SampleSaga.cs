using System;
using System.Threading.Tasks;

using Rebus.Sagas;
using Rebus.Bus;
using Rebus.Handlers;

namespace CvLab.MicroserviceTemplate1.MessageProcessing.Sagas
{
    internal class SampleSaga : Saga<SampleSagaData>,
        IAmInitiatedBy<InitMessage>,
        IHandleMessages<SampleMessage1>
    {
        public async Task Handle(InitMessage message)
        {
            Console.WriteLine("SampleSaga stated");
        }

        public async Task Handle(SampleMessage1 message)
        {
            Console.WriteLine("Message (from Saga) = {message}", message.Text);
        }

        protected override void CorrelateMessages(ICorrelationConfig<SampleSagaData> config)
        {
            config.Correlate<SampleMessage1>(m => m.CorrelationId, d => d.CorrelationId);
        }
    }
}
