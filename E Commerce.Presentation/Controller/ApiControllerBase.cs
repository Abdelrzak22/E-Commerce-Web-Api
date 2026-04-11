using E_Commerce.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controller
{

    [ApiController]
    [Route("api/[controller]")]
    public class ApiControllerBase:ControllerBase
    {
        //handle result wihtout value
        protected IActionResult HandleResult(Result result)
        {
            if (result.IsSuccess)
                return NoContent();
            else
               return HandleProblem(result.Errors);


        }
        //handle result wiht value

        protected ActionResult<Tvalue> HandleResult<Tvalue>(Result<Tvalue> result)
        {

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return HandleProblem(result.Errors);

        }

        private ActionResult HandleProblem(IReadOnlyList<Error> errors)
        {
            //if no error ,return server error
            if (errors.Count() == 0)
                return Problem(statusCode: StatusCodes.Status500InternalServerError, title: "an unexcepted error");



            //if all errors as validation errors, handle that  as validation problem
            if (errors.All(e => e.Type == ErrorType.Validation))
                return HandelValidationProblem(errors);
            // if only on error, handle single error
            return HandleSingleErrorProblem(errors[0]); //there are only one
        }

        private ActionResult HandleSingleErrorProblem (Error error)
        {
            return Problem(
                title: error.Code,
                detail: error.Description,
                type: error.Type.ToString(),
                statusCode: MapErrorTypeToStatusCode(error.Type));
        }

        private ActionResult HandelValidationProblem(IReadOnlyList<Error> errors)
        {
            var modelstate = new ModelStateDictionary();
            foreach (var error in errors)
                modelstate.AddModelError(error.Code, error.Description);
            return ValidationProblem(modelstate);
        }
        private static int MapErrorTypeToStatusCode(ErrorType errorType) => errorType switch
        {


            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.InvalidCredentials => StatusCodes.Status401Unauthorized,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _=>StatusCodes.Status500InternalServerError

        };

    }
}
