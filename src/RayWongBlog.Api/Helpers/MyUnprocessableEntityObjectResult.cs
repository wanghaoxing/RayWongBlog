using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RayWongBlog.Api.Helpers
{
    public class MyUnprocessableEntityObjectResult : UnprocessableEntityObjectResult
    {
        public MyUnprocessableEntityObjectResult(ModelStateDictionary modelState) : base(new ResoureceValidationResult(modelState))
        {
            if (modelState == null)
            {
                throw new ArgumentNullException();
            }
            StatusCode = 422;
        }


    }
}
