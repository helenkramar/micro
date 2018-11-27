using integration.Services;

namespace integration.Test
{
    using Pact;
    using Xunit.Abstractions;
    using ProviderService = Pact.ProviderService;

    public abstract class BaseConsumerTest
    {
        protected Infrastructure.Consumer.Consumer Consumer { get; set; }

        protected ProviderMock ProviderMock { get; set; }

        protected ITestOutputHelper OutputHelper { get; }

        //protected IMockProviderService _mockProviderService { get; set; }

        //protected dynamic _mockProviderServiceBaseUri { get; set; }

        public BaseConsumerTest()
        {
            
            StartMocks();
        }

        private void StartMocks()
        {
            var providerPort = ProviderService.ProviderMockEntity.MockPort;
            var pact = new ConsumerPact("Consumer", "Provider", providerPort);
            ProviderMock = new ProviderMock(pact);
        }
    }
}