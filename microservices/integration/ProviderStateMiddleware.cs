namespace integration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
    using Newtonsoft.Json;

    public class ProviderStateMiddleware
    {
        private const string ConsumerName = "Event API Consumer";
        private readonly Func<IDictionary<string, object>, Task> m_next;
        private readonly IDictionary<string, Action> _providerStates;

        public ProviderStateMiddleware(Func<IDictionary<string, object>, Task> next)
        {
            m_next = next;
            _providerStates = new Dictionary<string, Action>
            {
                {
                    "There is a something with id 'tester'",
                    AddTesterIfItDoesntExist
                }
            };
        }

        private void AddTesterIfItDoesntExist()
        {
            //Add code to go an inject or insert the tester data
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            IOwinContext context = new OwinContext(environment);

            if (context.Request.Path.Value == "/provider-states")
            {
                context.Response.StatusCode = (int)HttpStatusCode.OK;

                if (context.Request.Method == HttpMethod.Post.ToString() &&
                    context.Request.Body != null)
                {
                    string jsonRequestBody;
                    using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
                    {
                        jsonRequestBody = reader.ReadToEnd();
                    }

                    var providerState = JsonConvert.DeserializeObject<ProviderState>(jsonRequestBody);

                    //A null or empty provider state key must be handled
                    if (providerState != null &&
                        !IsNullOrEmpty(providerState.State) &&
                        providerState.Consumer == ConsumerName)
                    {
                        _providerStates[providerState.State].Invoke();
                    }

                    await context.Response.WriteAsync(Empty);
                }
            }
            else
            {
                await m_next.Invoke(environment);
            }
        }
    }
}