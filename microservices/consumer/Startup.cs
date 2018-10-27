using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using consumer.Client;
using consumer.Service;

namespace consumer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var provider = ProviderService.GetProvider("provider");

            services.AddHttpClient("items", c => { c.BaseAddress = provider.Uri; })
                .AddTypedClient(c => Refit.RestService.For<IItemsClient>(c));

            services.AddMvc()
                .AddApplicationPart(typeof(Controllers.ValuesController).Assembly)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddCors();
            //services.AddCors(options =>
            //{
            //    options.AddPolicy(
            //        "AllowAnyOrigin",
            //        builder => builder
            //            .AllowAnyOrigin()
            //            .AllowAnyMethod()
            //            .AllowAnyHeader());
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseCors();
            //app.UseCors("AllowAnyOrigin");
        }
    }
}
