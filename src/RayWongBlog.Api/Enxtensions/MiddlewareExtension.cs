using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace RayWongBlog.Api.Enxtensions
{
    public static class MiddlewareExtension
    {

        public static IServiceCollection AddMiddleware(this IServiceCollection services)
        {
            services.AddMvc(
                options =>
                {
                    options.ReturnHttpNotAcceptable = true;//启用内容协商,效果是请求头里添加的需要的返回类型,如果服务端不支持,就会返回406,例如API消费者请求的是application/xml格式的media type，而API只支持application/json

                    options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());//比如客户端请求Accept,application/xml,增加输出格式的支持
                    var outFormats = options.OutputFormatters.OfType<JsonOutputFormatter>().FirstOrDefault();
                    if (outFormats != null)
                    {
                        outFormats.SupportedMediaTypes.Add("application/vnd.raywongblog.hateoas+json");

                    }
                    var inFormats = options.InputFormatters.OfType<JsonInputFormatter>().FirstOrDefault();
                    if (inFormats != null)
                    {
                        inFormats.SupportedMediaTypes.Add("application/vnd.raywongblog.article.create+json");

                    }
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                }).AddFluentValidation(fv =>
                {
                //    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                //    fv.
                });
            services.AddHttpsRedirection(options =>
            {

                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 5001;
            });
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
                options.ExcludedHosts.Add("example.com");
                options.ExcludedHosts.Add("www.example.com");
            });
            return services;
        }
    }
}
