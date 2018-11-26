namespace modeling.Pact
{
    using System;
    using PactNet;
    using PactNet.Mocks.MockHttpService;

    public class Pact : IDisposable
    {
        public IMockProviderService MockProviderService { get; set; }

        protected IPactBuilder PactBuilder { get; set; }

        public int MockServerPort { get; protected set; }

        public Uri MockProviderServiceBaseUri { get; protected set; }

        protected Pact() { }

        public Pact(Uri uri, int mockPort)
        {
            MockProviderServiceBaseUri = uri;
            MockServerPort = mockPort;

            MockProviderService = PactBuilder.MockService(MockServerPort);
            MockProviderService.ClearInteractions(); //NOTE: Clears any previously registered interactions before the test is run
        }

        public string Consumer { get; protected set; }

        public string Provider { get; protected set; }

        public void Dispose()
        {
            PactBuilder.Build(); //NOTE: Will save the pact file once finished
        }
    }
}