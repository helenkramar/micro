namespace modeling.Tests
{
    using PactNet.Mocks.MockHttpService;

    public abstract class BaseConsumerTest
    {
         
        protected IMockProviderService _mockProviderService { get; set; }

        protected dynamic _mockProviderServiceBaseUri { get; set; }
    }
}