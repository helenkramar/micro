using provider;

//using  consumer;

using Microsoft.Extensions.Configuration;

namespace integration.Application
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        //protected override void Register(IApplicationBuilder app)
        //{
        //    app.UseMiddleware<ProviderStateMiddleware>();
        //}
    }
}
