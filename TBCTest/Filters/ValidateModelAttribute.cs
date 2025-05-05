using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TBCTest.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Title = "One or more validation errors occurred.",
                    Status = StatusCodes.Status400BadRequest
                };

                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
    }
}
