namespace modeling.Builders
{
    using System.Net;
    using Pact;
    using PactNet.Mocks.MockHttpService;
    using PactNet.Mocks.MockHttpService.Models;
    using Utils;

    public abstract class BaseMock
    {
        public Pact Pact { get; }

        protected IMockProviderService MockService { get; }

        protected BaseMock(Pact pact)
        {
            Pact = pact;
            MockService = Pact.MockProviderService;
        }

        public ProviderServiceRequest CreateRequestToProvider(HttpVerb method, string path)
        {
            var request = new RequestBuilder()
                .WithMethod(method)
                .Path(path)
                .Build();
            return request;
        }

        public ProviderServiceResponse CreateResponseFromProvider(HttpStatusCode statusCode, object responseBody, Header header)
        {
            var response = new ResponseBuilder()
                .WithStatus(statusCode)
                .Body(responseBody)
                .AddHeader(header)
                .Build();
            return response;
        }

        public void VerifyCallsAreReceivedByMock()
        {
            MockService.VerifyInteractions();
        }

        public void RemoveMockedInteractions()
        {
            MockService.ClearInteractions();
        }
    }
}