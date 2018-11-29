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
    public class ConsumerMyApiPact : IDisposable
    {
        public IPactBuilder PactBuilder { get; private set; }

        public IMockProviderService MockProviderService { get; private set; }

        public int MockServerPort { get; } = ProviderService.ProviderMock.MockPort;

        public ConsumerMyApiPact()
        {
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

            MockProviderService = PactBuilder.MockService(MockServerPort); //Configure the http mock server
        }

        public void Dispose()
        {
            PactBuilder.Build(); //NOTE: Will save the pact file once finished
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