using System;
using System.Collections.Generic;
using System.Net.Http;

namespace consumer.Base
{
    public class HttpRequest
    {
        /// <summary>Request builder</summary>
        //protected IHttpRequestBuilder<HttpWebRequest> HttpRequestBuilder = (IHttpRequestBuilder<HttpWebRequest>)new Ipreo.AutomationFramework.WebService.Http.HttpRequestBuilder();

        /// <summary>Initializes a new instance of HttpRequest</summary>
        public HttpRequest()
        {
            this.QueryParameters = (IList<NameValueParameter>)new List<NameValueParameter>();
            this.Parameters = (IList<NameValueParameter>)new List<NameValueParameter>();
        }

        /// <summary>
        /// Initializes a new instance of HttpRequest for the specified Url
        /// </summary>
        /// <param name="url">URL of the requested resource</param>
        public HttpRequest(string url)
          : this()
        {
            this.Url = new Uri(url);
        }

        /// <summary>
        /// Initializes a new instance of HttpRequest for the specified Url
        /// </summary>
        /// <param name="url">URL of the requested resource</param>
        public HttpRequest(Uri url)
          : this()
        {
            this.Url = url;
        }

        /// <summary>
        /// Gets or sets the Uniform Resource Identifier (URI) of the Internet resource that actually responds to the request
        /// </summary>
        public virtual Uri Url { get; set; }

        /// <summary>Gets or sets the method for the request</summary>
        public virtual HttpMethod Method { get; set; }


        /// <summary>Gets or sets the value of the Accept HTTP header.</summary>
        public string Accept { get; set; }

        /// <summary>
        /// Gets or sets the value of the Content-type HTTP header
        /// </summary>
        public virtual string ContentType { get; set; }

        /// <summary>Gets or sets the value of the UserAgent header</summary>
        public virtual string UserAgent { get; set; }

        /// <summary>
        /// Gets a collection of the name/value pairs that make up the HTTP request query parameters
        /// </summary>
        public virtual IList<NameValueParameter> QueryParameters { get; protected set; }

        /// <summary>
        /// Gets a collection of the name/value pairs that will be replaced in url and body
        /// </summary>
        public virtual IList<NameValueParameter> Parameters { get; protected set; }

        /// <summary>Gets or sets HTTP request data</summary>
        public virtual string Body { get; set; }

        /// <summary>Gets or sets HTTP request timeout in milliseconds</summary>
        public virtual int Timeout { get; set; }

        /// <summary>Executes HTTP request</summary>
        /// <param name="parameters">Collection of parameters that will be replaced in request url and body</param>
        /// <returns>HTTP response data</returns>
        //public virtual IHttpResponse Execute(params NameValueParameter[] parameters)
        //{
        //    foreach (NameValueParameter parameter in parameters)
        //        this.Parameters.Add(parameter);
        //    //HttpWebRequest httpWebRequest = this.HttpRequestBuilder.Build((IHttpRequest)this);
        //    WebException webException = (WebException)null;
        //    HttpWebResponse response;
        //    try
        //    {
        //        response = (HttpWebResponse)httpWebRequest.GetResponse();
        //    }
        //    catch (WebException ex)
        //    {
        //        Logger<>.Debug("Error occurred during http request execution", (Exception)ex, new object[0]);
        //        response = (HttpWebResponse)ex.Response;
        //        webException = ex;
        //    }
        //    return HttpResponse.Wrap(response, (Exception)webException);
        //}

        /// <summary>Executes HTTP request in async mode</summary>
        /// <param name="Handler">Function that will be executed with response after it is retrieved</param>
        /// <param name="parameters">Collection of parameters that will be replaced in request url and body</param>
        /// <returns>Task</returns>
        //public virtual async Task<IHttpResponse> ExecuteAsync(Action<IHttpResponse> Handler, params NameValueParameter[] parameters)
        //{
        //    return await Task.Factory.StartNew<IHttpResponse>((Func<IHttpResponse>)(() =>
        //    {
        //        IHttpResponse httpResponse = this.Execute(parameters);
        //        if (Handler != null)
        //            Handler(httpResponse);
        //        return httpResponse;
        //    }));
        //}

        /// <summary>Executes HTTP request in async mode</summary>
        /// <param name="parameters">Collection of parameters that will be replaced in request url and body</param>
        /// <returns>Task</returns>
        //public virtual async Task<IHttpResponse> ExecuteAsync(params NameValueParameter[] parameters)
        //{
        //    return await this.ExecuteAsync((Action<IHttpResponse>)null, parameters);
        //}

       

        /// <summary>
        /// Creates new IHttpRequest with specified url  and method GET
        /// </summary>
        /// <param name="url">URL of the requested resource</param>
        /// <returns>New instance of IHttpRequest</returns>
        public static HttpRequest Get(string url)
        {
            return HttpRequest.Create(url, HttpMethod.Get);
        }

        /// <summary>
        /// Creates new IHttpRequest with specified url  and method POST
        /// </summary>
        /// <param name="url">URL of the requested resource</param>
        /// <returns>New instance of IHttpRequest</returns>
        public static HttpRequest Post(string url)
        {
            return HttpRequest.Create(url, HttpMethod.Post);
        }

        /// <summary>
        /// Creates new IHttpRequest with specified url  and method PUT
        /// </summary>
        /// <param name="url">URL of the requested resource</param>
        /// <returns>New instance of IHttpRequest</returns>
        public static HttpRequest Put(string url)
        {
            return HttpRequest.Create(url, HttpMethod.Put);
        }

        /// <summary>
        /// Creates new IHttpRequest with specified url  and method DELETE
        /// </summary>
        /// <param name="url">URL of the requested resource</param>
        /// <returns>New instance of IHttpRequest</returns>
        public static HttpRequest Delete(string url)
        {
            return HttpRequest.Create(url, HttpMethod.Delete);
        }

        /// <summary>
        /// Creates new IHttpRequest with specified url and method
        /// </summary>
        /// <param name="url">URL of the requested resource</param>
        /// <param name="method">HTTP request method</param>
        /// <returns>New instance of IHttpRequest</returns>
        public static HttpRequest Create(string url, HttpMethod method)
        {
            return (HttpRequest)new HttpRequest(url)
            {
                Method = method
            };
        }

    }
}
