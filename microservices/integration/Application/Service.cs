using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using integration.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace integration
{
    public class Service : IDisposable
    {
        private readonly TestServer _testServer;

        public HttpClient Client { get; }

        public Service()
        {
            _testServer = FireupTestServer();
            Client = _testServer.CreateClient();

            Client.Timeout = TimeSpan.FromMinutes(5);
        }

        private TestServer FireupTestServer()
        {
            var builder = new WebHostBuilder()
                .UseConfiguration(new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .AddEnvironmentVariables()
                        .Build())
                .CaptureStartupErrors(true)
                .UseSetting("detailedErrors", "true")
                .UseStartup<TestStartup>();

            var server = new TestServer(builder);
            return server;
        }

        public async Task<T> GetAsync<T>(string requestUri)
        {
            return await SendRequest<T>(HttpMethod.Get, requestUri);
        }

        private async Task<TObject> SendRequest<TObject>(HttpMethod method, string requestUri, HttpContent requestContent = null)
        {
            var response = await SendRequest(method, requestUri, requestContent);

            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TObject>(content);
            }

            throw new Exception($"Status Code: {(int)response.StatusCode}\r\n" +
                                $"Endpoint: {response.RequestMessage.RequestUri}\r\n" +
                                $"Reason: {response.ReasonPhrase}\r\n" +
                                $"Response content: {content}");
        }

        private async Task<HttpResponseMessage> SendRequest(HttpMethod method, string requestUri, HttpContent requestContent = null)
        {
            using (var request = new HttpRequestMessage(method, requestUri))
            {
                request.Content = requestContent;
                return await Client.SendAsync(request);
            }
        }

        public void Dispose()
        {
            Client.Dispose();
            _testServer.Dispose();
        }
    }
}
