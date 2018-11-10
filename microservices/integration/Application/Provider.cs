using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace integration.Application
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
                .UseStartup<TestStartup>()
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
