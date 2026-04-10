using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_web.Factory
{
    public class ApiResoponeFactoryy
    {

        public static IActionResult GenerateApiValidation(ActionContext actionContext)
        {
            var errors = actionContext.ModelState.Where(e => e.Value.Errors.Count() > 0)
                    .ToDictionary(x => x.Key, x => x.Value.Errors.Select(x => x.ErrorMessage).ToArray());
            var problem = new ProblemDetails()
            {
                Title = "validation Errors",
                Detail = "one or more error occure",
                Status = StatusCodes.Status400BadRequest,
                Extensions = { { "Errors", errors } }
            };

            return new BadRequestObjectResult(problem);
        }
    }
}
