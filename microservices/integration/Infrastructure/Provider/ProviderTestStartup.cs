namespace integration.Infrastructure.Provider
{
    using Microsoft.Extensions.Configuration;
    using provider;

    public class ProviderTestStartup : Startup
    {
        public ProviderTestStartup(IConfiguration configuration) : base(configuration)
        {
        }
    }
}