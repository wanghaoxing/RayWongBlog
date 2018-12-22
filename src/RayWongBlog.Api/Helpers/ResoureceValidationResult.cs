using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RayWongBlog.Api.Helpers
{
    public class ResoureceValidationResult:Dictionary<string,IEnumerable<ResourceValidatonError>>
    {
        //public ResoureceValidationResult() : base(StringComparer.OrdinalIgnoreCase)
        //{

        //}
        public ResoureceValidationResult(ModelStateDictionary modelState) : base(StringComparer.OrdinalIgnoreCase)
        {

            if (modelState == null)
            {
                throw new ArgumentNullException(nameof(modelState));
            }
            foreach(var keyModelStatePair in modelState)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                if (errors != null && errors.Count > 0)
                {
                    var errorsAdd = new List<ResourceValidatonError>();
                    foreach(var error in errors)
                    {
                        var keyAddMessage = error.ErrorMessage.Split('|');
                        if (keyAddMessage.Length > 1)
                        {
                            errorsAdd.Add(new ResourceValidatonError(keyAddMessage[1], keyAddMessage[0]));
                        }
                        else
                        {
                            errorsAdd.Add(new ResourceValidatonError(keyAddMessage[0]));
                        }
                    }
                    Add(key, errorsAdd);
                }
            }
        }
    }
}
