﻿using System;
using System.IO;
using integration.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PactNet;
using PactNet.Mocks.MockHttpService;

namespace integration.Pact
{
    public class ConsumerMyApiPact : IDisposable
    {
        public IPactBuilder PactBuilder { get; private set; }

        public IMockProviderService MockProviderService { get; private set; }

        public int MockServerPort { get; } = ProviderService.ProviderMock.MockPort;
        public string MockProviderServiceBaseUri { get; } = ProviderService.ProviderMock.Uri.ToString();

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