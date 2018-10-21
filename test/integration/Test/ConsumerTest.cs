using System.Collections.Generic;
using System.Net.Http;
using integration.Models;
using integration.Pact;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using Xunit;

namespace integration.Test
{
    public class SomethingApiConsumerTests : IClassFixture<ConsumerMyApiPact>
    {
        private IMockProviderService _mockProviderService;
        dynamic _mockProviderServiceBaseUri;

        public SomethingApiConsumerTests(ConsumerMyApiPact data)
        {
            _mockProviderService = data.MockProviderService;
            _mockProviderService.ClearInteractions(); //NOTE: Clears any previously registered interactions before the test is run
            _mockProviderServiceBaseUri = data.MockProviderServiceBaseUri;
        }

        [Fact]
        public async void GetSomething_WhenTheTesterSomethingExists_ReturnsTheSomething()
        {
            //Arrange
            string path = "api/items";
            dynamic body = new[] { new Item { Id = 4, Name = "candy" }, new Item { Id = 5, Name = "cookie" } };

            _mockProviderService
                .Given("There is a items with id 'test'")
                .UponReceiving("A GET request to retrieve the items")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = $"/{path}",
                    Headers = new Dictionary<string, object>
                    {
                        //{ "Accept", "application/json" },
                        //{ "Content-Type", "application/json; charset=utf-8" }
                    }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = body
                }); //NOTE: WillRespondWith call must come last as it will register the interaction
            path = $@"{_mockProviderServiceBaseUri}{path}";

            //Act
            var result = await new Service().GetAsync<IAsyncEnumerable<Item>>(path);

            //Assert
            Assert.Equal(body, result);

            _mockProviderService.VerifyInteractions(); //NOTE: Verifies that interactions registered on the mock provider are called at least once
        }
    }
}
