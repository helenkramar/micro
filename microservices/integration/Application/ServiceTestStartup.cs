//using provider;

using  consumer;

using Microsoft.Extensions.Configuration;

namespace integration.Application
{
    public class ServiceTestStartup : Startup
    {
        public ServiceTestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        //protected override void Register(IApplicationBuilder app)
        //{
        //    app.UseMiddleware<ProviderStateMiddleware>();
        //}
    }
}