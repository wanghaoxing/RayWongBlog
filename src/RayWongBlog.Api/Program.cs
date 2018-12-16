using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RayWongBlog.Infrastructure.DataBase;
using Serilog;

namespace RayWongBlog.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            Log.Logger = new LoggerConfiguration()
                            .WriteTo.Console()
                            .WriteTo.Seq("http://localhost:5341")
                            .CreateLogger();
            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var loggerFactory = service.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = service.GetRequiredService<BlogContext>();
                    BlogContextSeed.SeedAsync(context, loggerFactory).Wait();
                }
                catch (Exception e)
                {

                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(e, "SeedAsync 失败");
                }
            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var assemblyName = typeof(Startup).GetTypeInfo().Assembly.FullName;

            return WebHost.CreateDefaultBuilder(args)
            .UseStartup(assemblyName)
            .UseSerilog();
            //怎么屏蔽不掉默认的输出提供程序
            //.ConfigureLogging((context, logging) =>
            //{
            //});

        }

    }
}
