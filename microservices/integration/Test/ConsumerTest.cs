using System.Collections.Generic;
using integration.Models;
using integration.Pact;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using Xunit;

namespace integration.Test
{
    public class ProviderConsumerTests : IClassFixture<ConsumerMyApiPact>
    {
        IMockProviderService _mockProviderService { get; set; }

        public ProviderConsumerTests(ConsumerMyApiPact data)
        {
            _mockProviderService = data.MockProviderService;
            _mockProviderService.ClearInteractions(); //NOTE: Clears any previously registered interactions before the test is run
        }

        [Fact]
        public async void GetSomething_WhenTheTesterSomethingExists_ReturnsTheSomething()
        {
            //Arrange
            string path = "/api/items";
            var body = new List<Item> { new Item { Id = 4, Name = "candy" }, new Item { Id = 5, Name = "cookie" } };

            _mockProviderService
                .Given("There are some items.")
                .UponReceiving("A GET request to retrieve the items.")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = path,
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

            var service = new Service();

            //Act
            var result = await service.GetAsync<IEnumerable<Item>>("/api/v1/it");

            //Assert
            Assert.Equal(body, result, new ItemsSame());

            _mockProviderService.VerifyInteractions(); //NOTE: Verifies that interactions registered on the mock provider are called at least once
        }
    }
}
