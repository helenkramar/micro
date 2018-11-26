using System.Collections.Generic;
using integration.Models;
using integration.Pact;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using Xunit;

namespace integration.Test.Consumer
{
    using System.Net;
    using modeling.Builders;
    using modeling.Tests;
    using modeling.Utils;

    public class SomethingApiConsumerTests : BaseConsumerTest, IClassFixture<ConsumerPact>
    {
        public SomethingApiConsumerTests(ConsumerPact data)
        {
            _mockProviderService = data.MockProviderService;
            _mockProviderServiceBaseUri = data.MockProviderServiceBaseUri;
        }

        [Fact]
        public async void ReGetSomething()
        {
            //Arrange
            string path = "/api/items";
            var body = new List<Item> { new Item { Id = 4, Name = "candy" }, new Item { Id = 5, Name = "cookie" } };

            string providerState = "There are some items.";
            string description = "A GET request to retrieve the items.";

            var request = new BaseMock().CreateRequestToProvider(HttpVerb.Get, path);
            var response = new BaseMock().CreateResponseFromProvider(HttpStatusCode.OK, body, new Header(HttpRequestHeader.ContentType, ContentType.Json));

            

            //_mockProviderService.
            //    .Given()
            //    .UponReceiving("A GET request to retrieve the items.")
            //    .With(new ProviderServiceRequest
            //    {
            //        Method = HttpVerb.Get,
            //        Path = path,
            //    })
            //    .WillRespondWith(new ProviderServiceResponse
            //    {
            //        Status = 200,
            //        Headers = new Dictionary<string, object>
            //        {
            //            { "Content-Type", ContentType.Json }
            //        },
            //        Body = body
            //    }); //NOTE: WillRespondWith call must come last as it will register the interaction

            var service = new Service();
            //Act
            var result = await service.GetAsync<IEnumerable<Item>>("/api/v1/it");

            //Assert
            Assert.Equal(body, result, new ItemsSame());

            _mockProviderService.VerifyInteractions(); //NOTE: Verifies that interactions registered on the mock provider are called at least once
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
                        { "Content-Type", ContentType.Json }
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
