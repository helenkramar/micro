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
            this.MockServerPort = ProviderService.ProviderMockEntity.MockPort;
            //PactBuilder = new PactBuilder(new PactConfig { PactDir = @"..\pacts", LogDir = @"..\pact_logs" }); //Configures the PactDir and/or LogDir.

            PactBuilder = new PactBuilder(new PactConfig
            {
                PactDir = @".\pacts",
                LogDir = @".\pact_logs",
                SpecificationVersion = "2.0.0"
            });


            PactBuilder
              .ServiceConsumer("Consumer")
              .HasPactWith("Provider");

            //MockProviderService = PactBuilder.MockService(MockServerPort); //Configure the http mock server
        }

        public ConsumerPact(string consumer, string provider, int mockServerPort) : base(mockServerPort)
        {
            Consumer = consumer;
            Provider = provider;

            PactBuilder = new PactBuilder(new PactConfig
            {
                PactDir = @".\pacts",
                LogDir = @".\pact_logs",
                SpecificationVersion = "2.0.0"
            });

            PactBuilder
                .ServiceConsumer(Consumer)
                .HasPactWith(Provider);

            MockProviderService = PactBuilder.MockService(MockServerPort);
            MockProviderService.ClearInteractions(); //NOTE: Clears any previously registered interactions before the test is run
        }
    }

    public class ProviderService
    {
        public static ProviderMockEntity ProviderMockEntity { get; } = GetProvider("provider");

        private const string AppSettings = "appsettings.json";

        private static ProviderMockEntity GetProvider(string value)
        {
            var envFile = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppSettings));
            return JsonConvert.DeserializeObject<JObject>(envFile).GetValue(value).ToObject<ProviderMockEntity>();
        }
    }
}