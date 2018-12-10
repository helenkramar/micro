using provider;

//using  consumer;

using Microsoft.Extensions.Configuration;

namespace integration.Application
{
    public class ProviderTestStartup : Startup
    {
        public ProviderTestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        //protected override void Register(IApplicationBuilder app)
        //{
        //    app.UseMiddleware<ProviderStateMiddleware>();
        //}
    }
}
