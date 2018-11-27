using consumer; //change here to provider

using Microsoft.Extensions.Configuration;

namespace integration.Infrastructure.Consumer
{
    public class ConsumerTestStartup : Startup
    {
        public ConsumerTestStartup(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
