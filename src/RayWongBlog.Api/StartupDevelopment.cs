using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RayWongBlog.Api.Enxtensions;
using RayWongBlog.Domain.Interfaces.Repositorys;
using RayWongBlog.Domain.Models.ViewModels;
using RayWongBlog.Infrastructure.DataBase;
using RayWongBlog.Infrastructure.DataBase.Validators;
using RayWongBlog.Infrastructure.Repositorys;
using RayWongBlog.Infrastructure.Services;

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
            //标准写法
            //var connection = Configuration["ConnectionStrings:DefaultConnection"];
            //简化写法
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BlogContext>(options =>
            {
                options.UseSqlite(connection);
            });
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper();

            //生成前一页下一页链接需要用到的,就是这样写
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(factory =>
            {
                var actionContext = factory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });
            var propertyMappingContainer = new PropertyMappingContainer();
            propertyMappingContainer.Register<ArticlePropertyMapping>();
            services.AddSingleton<IPropertyMappingContainer>(propertyMappingContainer);
            services.AddTransient<IValidator<ArticleAddViewModel>, ArticleValidator<ArticleAddViewModel>>();
            services.AddTransient<IValidator<ArticleUpdateViewModel>, ArticleValidator<ArticleUpdateViewModel>>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandle(loggerFactory);
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
