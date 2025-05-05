using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TBCTest.LocalizationSupport;
using TBCTest.Services;

namespace TBCTest.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var localizer = context.HttpContext.RequestServices
                                      .GetRequiredService<IDbLocalizationService>();

                var errors = context.ModelState
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(error =>
                            string.IsNullOrWhiteSpace(error.ErrorMessage)
                                ? localizer.Get(AppMessages.RequiredField)
                                : localizer.Get(error.ErrorMessage)
                        ).ToArray()
                    );

                var problemDetails = new ValidationProblemDetails(errors)
                {
                    Title = localizer.Get(AppMessages.ValidationError),
                    Status = StatusCodes.Status400BadRequest
                };

                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
    }
}
