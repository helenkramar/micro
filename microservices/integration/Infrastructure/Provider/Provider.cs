using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using integration.Infrastructure.Consumer;

namespace integration.Infrastructure.Provider
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
                .UseStartup<ProviderTestStartup>()
                //.UseSetting("detailedErrors", "true")
                .UseUrls(url)
                .Build()
                .Start();
        }

        public void Dispose()
        {
            _host.Dispose();
        }
    }
}
