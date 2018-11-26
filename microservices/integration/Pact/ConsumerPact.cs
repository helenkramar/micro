using System;
using System.Collections.Generic;
using System.IO;
using integration.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PactNet;
using PactNet.Infrastructure.Outputters;
using PactNet.Mocks.MockHttpService;
using Xunit.Abstractions;

namespace integration.Pact
{
    using modeling.Pact;

    public class ConsumerPact : Pact
    {
        public ConsumerPact()
        {
            this.MockServerPort = ProviderService.ProviderMock.MockPort;
            this.MockProviderServiceBaseUri = ProviderService.ProviderMock.Uri;
            //PactBuilder = new PactBuilder(new PactConfig { PactDir = @"..\pacts", LogDir = @"..\pact_logs" }); //Configures the PactDir and/or LogDir.

            PactBuilder = new PactBuilder(new PactConfig
            {
                PactDir = @".\pacts",
                LogDir = @".\pact_logs",
                SpecificationVersion = "2.0.0"
            });


            PactBuilder
              .ServiceConsumer("Consumer")
              .HasPactWith("Something API");

            MockProviderService = PactBuilder.MockService(MockServerPort); //Configure the http mock server
        }

        public ConsumerPact(string consumer, string provider, int mockServerPort, ITestOutputHelper output)
        {
            Consumer = consumer;
            Provider = provider;

            PactBuilder
                .ServiceConsumer(Consumer)
                .HasPactWith(Provider);
        }
    }

    public class ProviderService
    {
        public static ProviderMock ProviderMock { get; } = GetProvider("provider");

        private const string AppSettings = "appsettings.json";

        private static ProviderMock GetProvider(string value)
        {
            var envFile = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppSettings));
            return JsonConvert.DeserializeObject<JObject>(envFile).GetValue(value).ToObject<ProviderMock>();
        }
    }
}