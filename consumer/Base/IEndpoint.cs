using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace consumer.Base
{
    public class Endpoint
    {
        protected HttpMethod _method;
        protected string _partialUrl;
        protected Uri _uri;

        /// <summary>Initializes Endpoint</summary>
        public Endpoint()
        {

        }

        /// <summary>Initializes Endpoint with url</summary>
        public Endpoint(string partialUrl)
          : this()
        {
            this.PartialUrl = partialUrl;
        }


        /// <summary>Gets the Uniform Resource Identifier (URI)</summary>
        public virtual Uri Url
        {
            get { return this._uri; }
        }

        /// <summary>Gets or sets part of url</summary>
        /// <example>
        /// full url is http://example.com:8000/api/resource/1
        /// endpoint partial url will be: '/1'
        /// </example>
        public virtual string PartialUrl
        {
            get
            {
                return this._partialUrl;
            }
            set
            {
                this._partialUrl = value;
            }
        }

        /// <summary>Gets or sets HTTP method</summary>
        public virtual HttpMethod Method
        {
            get
            {
                return this.Method;
            }
            set
            {
                this.Method = value;
            }
        }



        /// <summary>
        /// Builds instance of IHttpRequest from IEndpoint metadata
        /// </summary>
        /// <returns>Instance of IHttpRequest</returns>
        public virtual HttpRequest BuildRequest()
        {
            //HttpRequest request = this.RequestTemplate.Clone();
            //IServiceClient client = this.GetClient((object)null);
            //if (client is IHasAuthentication && ((IHasAuthentication)client).Authentication != null)
            //    ((IHasAuthentication)client).Authenticate(request);
            return new HttpRequest(); //request;
        }

        /// <summary>Executes HTTP request to endpoint</summary>
        /// <param name="parameters">Collection of parameters</param>
        /// <returns>IHttpResponse got from service endpoint</returns>
        //public virtual IHttpResponse Invoke(params NameValueParameter[] parameters)
        //{
        //    return this.BuildRequest().Execute(parameters);
        //}

        /// <summary>Executes HTTP request to endpoint asynchronously</summary>
        /// <param name="parameters">Collection of parameters</param>
        /// <returns>IHttpResponse got from service endpoint</returns>
        //public virtual async Task<IHttpResponse> InvokeAsync(params NameValueParameter[] parameters)
        //{
        //    return await Task.Factory.StartNew<IHttpResponse>((Func<IHttpResponse>)(() => this.Invoke(parameters)));
        //}
    }
}