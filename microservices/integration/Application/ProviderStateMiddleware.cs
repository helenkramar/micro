namespace integration.Application
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
    using Microsoft.Owin;
    using Newtonsoft.Json;

    public class ProviderStateMiddleware
    {
        private readonly IDictionary<string, Action> _providerStates;
        private readonly Func<IDictionary<string, object>, Task> _next;

        public ProviderStateMiddleware(Func<IDictionary<string, object>, Task> next)
        {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            IOwinContext context = new OwinContext(environment);

            //HttpContext context = new DefaultHttpContext((IFeatureCollection)environment);

            if (context.Request.Path.Value == "/provider-states")
            {
                context.Response.StatusCode = (int)HttpStatusCode.OK;

                if (context.Request.Method == HttpMethod.Post.ToString() && context.Request.Body != null)
                {
                    string jsonRequestBody;
                    using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
                    {
                        jsonRequestBody = reader.ReadToEnd();
                    }

                    var providerState = JsonConvert.DeserializeObject<ProviderState>(jsonRequestBody);
                    //A null or empty provider state key must be handled
                    if (!string.IsNullOrEmpty(providerState.State))
                    {
                        _providerStates[providerState.State].Invoke();
                    }

                    await context.Response.WriteAsync(String.Empty);
                }
            }
            else
            {
                await _next.Invoke(environment);
            }
        }
    }

    public class ProviderState
    {
        public string Consumer { get; set; }
        public string State { get; set; }
    }

}