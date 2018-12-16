using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RayWongBlog.Api.Enxtensions
{
    public static class GlobalExceptionHandleExtension
    {
        public static void UseExceptionHandle(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/text";
                    var ex = context.Features.Get<IExceptionHandlerFeature>();
                    if (ex != null)
                    {
                        var logger = loggerFactory.CreateLogger("RayWongBlog.Api.Enxtensions.GlobalExceptionHandleExtension");
                        logger.LogError(500, ex.Error, ex.Error.Message);
                    }
                    await context.Response.WriteAsync(ex?.Error?.Message ?? "An Error Occurred.");

                });
            });
        }
    }
}
