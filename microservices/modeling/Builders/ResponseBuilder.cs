namespace modeling.Builders
{
    using System.Collections.Generic;
    using System.Net;
    using PactNet.Mocks.MockHttpService.Models;
    using Utils;

    public class ResponseBuilder
    {
        private readonly ProviderServiceResponse _response;

        public ResponseBuilder()
        {
            _response = new ProviderServiceResponse();
        }

        public ResponseBuilder Body(object body)
        {
            _response.Body = body;
            return this;
        }

        public ResponseBuilder WithStatus(HttpStatusCode status)
        {
            _response.Status = (int)status;
            return this;
        }

        public ResponseBuilder AddHeader(string header, object value)
        {
            if (_response.Headers == null)
            {
                _response.Headers = new Dictionary<string, object>();
            }

            _response.Headers[header] = value;
            return this;
        }

        //public ResponseBuilder AddHeader(KeyValuePair<string, object> header)
        //{
        //    if (_response.Headers == null)
        //    {
        //        _response.Headers = new Dictionary<string, object>();
        //    }

        //    _response.Headers.Add(header);
        //    return this;
        //}

        public ResponseBuilder AddHeader(Header header)
        {
            return AddHeader(header.Name, header.Value);
        }

        public ProviderServiceResponse Build()
            => _response;
    }
}