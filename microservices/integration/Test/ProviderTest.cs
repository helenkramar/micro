using System.Collections.Generic;
using PactNet;
using PactNet.Infrastructure.Outputters;
using Xunit;
using System;
using System.Net.Http;
using Xunit.Abstractions;

namespace integration.Test.Provider
{
    using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;

    public class ProviderTests
    {
        private readonly ITestOutputHelper _output;

        public ProviderTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void EnsureSomethingApiHonoursPactWithConsumer()
        {
            //Arrange
            const string serviceUri = "http://localhost:9223";
            var config = new PactVerifierConfig
            {
                Outputters = new List<IOutput> //NOTE: We default to using a ConsoleOutput, however xUnit 2 does not capture the console output, so a custom outputter is required.
                {
                    new XUnitOutput(_output)
                },
                //CustomHeader = new KeyValuePair<string, string>("Authorization", "Basic VGVzdA=="), //This allows the user to set a request header that will be sent with every request the verifier sends to the provider
                Verbose = true //Output verbose verification logs to the test output
            };

            var service = new integration.Infrastructure.Provider.Provider(serviceUri);

            IPactVerifier pactVerifier = new PactVerifier(config);
            pactVerifier
                .ServiceProvider("Provider", serviceUri)
                .HonoursPactWith("Consumer")
                .PactUri(@"D:\univer\micro\microservices\integration\bin\Debug\netcoreapp2.1\pacts\consumer-provider.json")
                .Verify(providerState: "There are some items.", description: "A GET request to retrieve the items.");
        }

        [Fact]
        public void ProviderHappyPathOIntegrationTest()
        {
            const string serviceUri = "http://localhost:9222";

            var testServer = new integration.Infrastructure.Provider.Provider(serviceUri).Client;
            var response = testServer.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(serviceUri + "/api/items")
            }).Result.Content.ReadAsStringAsync().Result;

        }
    }
}