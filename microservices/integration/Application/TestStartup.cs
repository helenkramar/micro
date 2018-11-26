using consumer; //change here to provider

using Microsoft.Extensions.Configuration;

namespace integration.Application
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
