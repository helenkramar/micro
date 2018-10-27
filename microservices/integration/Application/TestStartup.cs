using consumer;

using Microsoft.Extensions.Configuration;

namespace integration.Application
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public void Configuration(IAppBuilder app)
        {
            var apiStartup = new Startup(); //This is your standard OWIN startup object
            app.Use<ProviderStateMiddleware>();

            apiStartup.Configuration(app);
        }
    }
}
