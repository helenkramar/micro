namespace modeling.Pact
{
    using System;
    using PactNet.Mocks.MockHttpService;
    using PactNet.Mocks.MockHttpService.Models;

    public static class MockExtension
    {
        public static void Mock(this IMockProviderService mock, ProviderServiceRequest request, ProviderServiceResponse response, string providerState = null, string description = null)
        {
            mock
                .Given(providerState ?? Guid.NewGuid().ToString())
                .UponReceiving(description ?? Guid.NewGuid().ToString())
                .With(request)
                .WillRespondWith(response);
        }
    }
}