namespace modeling.Builders
{
    using System.Collections.Generic;
    using PactNet.Mocks.MockHttpService.Models;

    public class RequestBuilder
    {
        private readonly ProviderServiceRequest _request;

        public RequestBuilder()
        {
            _request = new ProviderServiceRequest();
        }

        public RequestBuilder Path(object path)
        {
            _request.Path = path;
            return this;
        }

        public RequestBuilder Body(object body)
        {
            _request.Body = body;
            return this;
        }

        public RequestBuilder WithMethod(HttpVerb method)
        {
            _request.Method = method;
            return this;
        }

        public RequestBuilder AddHeader(string header, string value)
        {
            if (_request.Headers == null)
            {
                _request.Headers = new Dictionary<string, object>();
            }

            _request.Headers[header] = value;
            return this;
        }

        public RequestBuilder WithQuery(object query)
        {
            _request.Query = query;
            return this;
        }

        public ProviderServiceRequest Build()
            => _request;
    }
}