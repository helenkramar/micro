using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using consumer.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
            services.AddHttpClient("items", c =>
                {
                    c.BaseAddress = new Uri("http://localhost:50221");

                })
                .AddTypedClient(c => Refit.RestService.For<IItemsClient>(c));

            //services.AddHttpClient("hello", c =>
            //    {
            //        c.BaseAddress = new Uri("http://localhost:50221");
            //    })
            //    .AddTypedClient(c => Refit.RestService.For<IHelloClient>(c));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
