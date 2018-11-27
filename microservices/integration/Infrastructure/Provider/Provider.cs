using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using integration.Infrastructure.Consumer;

namespace integration.Infrastructure.Provider
{
    public class Provider : IDisposable
    {
        private readonly IWebHost _host;

        public Provider(string url)
        {
            _host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .CaptureStartupErrors(true)
                .UseStartup<ProviderTestStartup>()
                .UseSetting("detailedErrors", "true")
                .UseUrls(url)
                .Build();

            _host.Start();
        }

        public void Dispose()
        {
            _host.Dispose();
        }
    }
}
