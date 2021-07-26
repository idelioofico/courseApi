using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using product_categoryApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace product_categoryApi.Filter
{
    public class ModelStateValidationFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                //Intercept request and validates the Model data and return Result Object
                var FieldValidation = new ValidationErrorVMOutput(context.ModelState.SelectMany(errors => errors.Value.Errors).Select(error => error.ErrorMessage));
                context.Result = new BadRequestObjectResult(FieldValidation);
            }
        }
    }
}
