using provider;

using Microsoft.Extensions.Configuration;

namespace integration.Application
{
    using Microsoft.AspNetCore.Builder;
    using Owin;

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
