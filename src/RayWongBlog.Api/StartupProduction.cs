using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RayWongBlog.Api.Enxtensions;
using System;

namespace RayWongBlog.Api
{
    public class StartupProduction
    {
        public StartupProduction(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMiddleware();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseHsts();

        }
    }
}
