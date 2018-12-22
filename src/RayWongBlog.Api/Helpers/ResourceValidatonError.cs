using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RayWongBlog.Api.Helpers
{
    public class ResourceValidatonError
    {
        public string ValidatorKey { get; private set; }
        public string Message { get; private set; }
        public ResourceValidatonError(string message,string validatorKey = "")
        {
            validatorKey = ValidatorKey;
            Message = message;

        }
    }
}
