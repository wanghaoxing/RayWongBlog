using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace RayWongBlog.Api.Enxtensions
{
    public static class MiddlewareExtension
    {

        public static IServiceCollection AddMiddleware(this IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            return services;
        }
    }
}
