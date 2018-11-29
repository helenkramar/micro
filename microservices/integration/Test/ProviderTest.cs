using System.Collections.Generic;
using integration.Application;
using PactNet;
using PactNet.Infrastructure.Outputters;
using Xunit;
using Xunit.Sdk;

namespace integration.Test
{
    using System;
    using System.Net.Http;
    using Xunit.Abstractions;

    public class SomethingApiTests
    {
        private readonly ITestOutputHelper _output;

        public SomethingApiTests(ITestOutputHelper output)
        {
            _output = output;
        }
      

        [Fact]
        public void EnsureSomethingApiHonoursPactWithConsumer()
        {
            //Arrange
            const string serviceUri = "http://localhost:9222";
            var config = new PactVerifierConfig
            {
                Outputters = new List<IOutput> //NOTE: We default to using a ConsoleOutput, however xUnit 2 does not capture the console output, so a custom outputter is required.
                {
                    new XUnitOutput(_output)
                },
                //CustomHeader = new KeyValuePair<string, string>("Authorization", "Basic VGVzdA=="), //This allows the user to set a request header that will be sent with every request the verifier sends to the provider
                Verbose = true //Output verbose verification logs to the test output
            };

            //using (WebApp.Start<TestStartup>(serviceUri))

            //Act / Assert

            var testServer = new Provider(serviceUri);
            IPactVerifier pactVerifier = new PactVerifier(config);
                pactVerifier
                    //.ProviderState($"{serviceUri}/provider-states")
                    .ServiceProvider("Something API", serviceUri)
                    .HonoursPactWith("Consumer")
                    .PactUri(@"D:\univer\micro\microservices\integration\bin\Debug\netcoreapp2.1\pacts\consumer-something_api.json")
                    //or
                    //.PactUri("http://pact-broker/pacts/provider/Something%20Api/consumer/Consumer/latest") //You can specify a http or https Uri
                                                                                                           //or
                    //.PactUri("http://pact-broker/pacts/provider/Something%20Api/consumer/Consumer/latest", new PactUriOptions("someuser", "somepassword")) //You can also specify http/https basic auth details
                    .Verify(providerState: "There are some items.");

            //using (var provider = new integration.Application.Provider(serviceUri))
            //{

            //}
        }

        [Fact]
        public void ProviderHappyPathOIntegrationTest()
        {
            // var testServer = Pro
            const string serviceUri = "http://localhost:9222";

            var testServer = new Provider(serviceUri).Client;
            var response = testServer.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(serviceUri + "/api/items")
            }).Result.Content.ReadAsStringAsync().Result;

        }
    }
}