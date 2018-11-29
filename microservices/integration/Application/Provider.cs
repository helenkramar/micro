using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace integration.Application
{
    using System.Net.Http;
    using Microsoft.AspNetCore.TestHost;

    public class Provider : IDisposable
    {
        private IWebHost _host { get; set; }

        public HttpClient Client { get; set; }

        public Provider(string url)
        {
            new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .CaptureStartupErrors(true)
                .UseStartup<TestStartup>()
                .UseIISIntegration()
                //.UseSetting("detailedErrors", "true")
                .UseUrls(url)
                .Build()
                .Start();

            //testServer = StartTestServer();
            //_host = _builder.Build();
            //_host.Start();
        }

        public void Dispose()
        {
            _host.Dispose();
        }
    }
}
