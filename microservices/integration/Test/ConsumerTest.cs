using System.Collections.Generic;
using integration.Models;
using integration.Pact;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using Xunit;

using System.Net;
using modeling.Builders;
using modeling.Pact;
using modeling.Utils;

namespace integration.Test.Consumer
{
    public class ProviderConsumerTests : BaseConsumerTest
    {
        public ProviderConsumerTests()
        {
            Consumer = new Infrastructure.Consumer.Consumer();
            //_mockProviderService = data.MockProviderService;
            //_mockProviderService.ClearInteractions(); //NOTE: Clears any previously registered interactions before the test is run
        }

        //public SomethingApiConsumerTests(PactGenerator data)
        //{
        //    Consumer = new Infrastructure.Consumer.Consumer();
        //    data.SetPact(ProviderMock.Pact);
        //    //_mockProviderService = data.MockProviderService;
        //    //_mockProviderServiceBaseUri = data.MockProviderServiceBaseUri;
        //}

        [Fact]
        public async void ReGetSomething()
        {
            //Arrange
            string path = "/api/items";
            var body = new List<Item> { new Item { Id = 4, Name = "candy" }, new Item { Id = 5, Name = "cookie" } };

            string providerState = "There are some items.";
            string description = "A GET request to retrieve the items.";

            var request = new RequestBuilder()
                .Path(path)
                .WithMethod(HttpVerb.Get)
                .Build();


            var response = new ResponseBuilder()
                .Body(body)
                .WithStatus(HttpStatusCode.OK)
                .AddHeader("Content-Type", ContentType.Json)
                .Build();

            ProviderMock.MockService.Mock(request, response, providerState, description);
            
            //Act
            var result = await Consumer.GetAsync<IEnumerable<Item>>("/api/v1/it");

            //Assert
            Assert.Equal(body, result, new ItemsSame());

            ProviderMock.VerifyCallsAreReceivedByMock(); //NOTE: Verifies that interactions registered on the mock provider are called at least once
        }

        //[Fact]
        //public async void GetSomething_WhenTheTesterSomethingExists_ReturnsTheSomething()
        //{
        //    //Arrange
        //    string path = "/api/items";
        //    var body = new List<Item> { new Item { Id = 4, Name = "candy" }, new Item { Id = 5, Name = "cookie" } };

        //    _mockProviderService
        //        .Given("There are some items.")
        //        .UponReceiving("A GET request to retrieve the items.")
        //        .With(new ProviderServiceRequest
        //        {
        //            Method = HttpVerb.Get,
        //            Path = path,
        //        })
        //        .WillRespondWith(new ProviderServiceResponse
        //        {
        //            Status = 200,
        //            Headers = new Dictionary<string, object>
        //            {
        //                { "Content-Type", ContentType.Json }
        //            },
        //            Body = body
        //        }); //NOTE: WillRespondWith call must come last as it will register the interaction

        //    var service = new Consumer();
        //    //Act
        //    var result = await service.GetAsync<IEnumerable<Item>>("/api/v1/it");

        //    //Assert
        //    Assert.Equal(body, result, new ItemsSame());

        //    _mockProviderService.VerifyInteractions(); //NOTE: Verifies that interactions registered on the mock provider are called at least once
        //}
    }
}
