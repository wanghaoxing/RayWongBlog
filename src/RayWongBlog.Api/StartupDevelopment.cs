using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RayWongBlog.Api.Enxtensions;
using RayWongBlog.Domain.Interfaces.Repositorys;
using RayWongBlog.Infrastructure.DataBase;
using RayWongBlog.Infrastructure.Repositorys;

namespace RayWongBlog.Api
{
    public class StartupDevelopment
    {
        public StartupDevelopment(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMiddleware();
            var connection = "Data Source=RayWongBlog.db";
            services.AddDbContext<BlogContext>(options =>
            {
                options.UseSqlite(connection);
            });
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
