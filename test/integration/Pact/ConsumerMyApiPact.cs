using System;
using Newtonsoft.Json;
using PactNet;
using PactNet.Mocks.MockHttpService;
using PactNet.Models;

namespace integration.Pact
{
    public class ConsumerMyApiPact : IDisposable
    {
        public IPactBuilder PactBuilder { get; private set; }

        public IMockProviderService MockProviderService { get; private set; }

        public int MockServerPort { get; } = 9222;

        public string MockProviderServiceBaseUri { get { return $"http://localhost:{MockServerPort}"; } }

        public ConsumerMyApiPact()
        {
            PactBuilder = new PactBuilder(new PactConfig { PactDir = @"..\pacts", LogDir = @"..\pact_logs" }); //Configures the PactDir and/or LogDir.

            PactBuilder
              .ServiceConsumer("Consumer")
              .HasPactWith("Something API");

            MockProviderService = PactBuilder.MockService(MockServerPort); //Configure the http mock server
        }

        public void Dispose()
        {
            PactBuilder.Build(); //NOTE: Will save the pact file once finished
        }
    }
}