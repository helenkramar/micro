using System;
using System.Net.Http;

namespace consumer.Base
{
    public class EndpointAttribute : Attribute
    {
        /// <summary>Gets or Sets endpoint address</summary>
        public string PartialUrl { get; set; }

        /// <summary>Gets or Sets HTTP method</summary>
        public HttpMethod Method { get; set; }

        /// <summary>Sets default HTTP method to request for endpoint</summary>
        /// <param name="endpoint">IEndpoint to update</param>
        public void Apply(Endpoint endpoint)
        {
            endpoint.Method = this.Method;
            endpoint.PartialUrl = this.PartialUrl;
        }
    }
}