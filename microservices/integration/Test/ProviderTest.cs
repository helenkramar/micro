using System.Collections.Generic;
using integration.Application;
using PactNet;
using PactNet.Infrastructure.Outputters;
using Xunit;
using Xunit.Sdk;

namespace integration.Test
{
    public class SomethingApiTests
    {
        [Fact]
        public void EnsureSomethingApiHonoursPactWithConsumer()
        {
            //Arrange
            const string serviceUri = "http://localhost:9222";
            var config = new PactVerifierConfig
            {
                Outputters = new List<IOutput> //NOTE: We default to using a ConsoleOutput, however xUnit 2 does not capture the console output, so a custom outputter is required.
                {
                    new XUnitOutput(new TestOutputHelper())
                },
                CustomHeader = new KeyValuePair<string, string>("Authorization", "Basic VGVzdA=="), //This allows the user to set a request header that will be sent with every request the verifier sends to the provider
                Verbose = true //Output verbose verification logs to the test output
            };

            using (WebApp.Start<TestStartup>(serviceUri))
            {
                //Act / Assert
                IPactVerifier pactVerifier = new PactVerifier(config);
                pactVerifier
                    .ProviderState($"{serviceUri}/provider-states")
                    .ServiceProvider("Something API", serviceUri)
                    .HonoursPactWith("Consumer")
                    .PactUri("..\\..\\..\\Consumer.Tests\\pacts\\consumer-something_api.json")
                    //or
                    .PactUri("http://pact-broker/pacts/provider/Something%20Api/consumer/Consumer/latest") //You can specify a http or https Uri
                                                                                                           //or
                    .PactUri("http://pact-broker/pacts/provider/Something%20Api/consumer/Consumer/latest", new PactUriOptions("someuser", "somepassword")) //You can also specify http/https basic auth details
                    .Verify();
            }
        }
    }
}