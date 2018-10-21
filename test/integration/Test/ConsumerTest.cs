using System;
using System.Collections.Generic;
using System.Text;
using consumer;
using integration.Models;
using integration.Pact;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using Xunit;

namespace integration.Test
{
    public class SomethingApiConsumerTests : IClassFixture<ConsumerMyApiPact>
    {
        private IMockProviderService _mockProviderService;
        private string _mockProviderServiceBaseUri;

        public SomethingApiConsumerTests(ConsumerMyApiPact data)
        {
            _mockProviderService = data.MockProviderService;
            _mockProviderService.ClearInteractions(); //NOTE: Clears any previously registered interactions before the test is run
            _mockProviderServiceBaseUri = data.MockProviderServiceBaseUri;
        }

        //static IWebHostBuilder CreateWebHostBuilder() =>
        //    WebHost.CreateDefaultBuilder()
        //        .UseStartup<Startup>();

        [Fact]
        public void GetSomething_WhenTheTesterSomethingExists_ReturnsTheSomething()
        {
            //Arrange
            _mockProviderService
                .Given("There is a something with id 'tester'")
                .UponReceiving("A GET request to retrieve the something")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = "/api/items/2",
                    Headers = new Dictionary<string, object>
                    {
                        { "Accept", "application/json" }
                    }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = new //NOTE: Note the case sensitivity here, the body will be serialised as per the casing defined
                    {
                        id = "2",
                        name = "Book"
                    }
                }); //NOTE: WillRespondWith call must come last as it will register the interaction

            //var consumer = CreateWebHostBuilder().Build();//.Run(); //new Service(_mockProviderServiceBaseUri); 

            //Act
            var service = new Service();
            var result = service.GetAsync<Item>("/api/items/2");

            //Assert
            Assert.Equal("2", result.Id.ToString());

            _mockProviderService.VerifyInteractions(); //NOTE: Verifies that interactions registered on the mock provider are called at least once
        }
    }
}
