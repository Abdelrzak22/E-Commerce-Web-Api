using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_web.CustomMiddleWare
{
    public class ExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleWare> _logger;

        public ExceptionHandlerMiddleWare(RequestDelegate next,ILogger<ExceptionHandlerMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);

                if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    var problem = new ProblemDetails()
                    {
                        Title = "Error while make this request endpoint not found",
                        Status = StatusCodes.Status404NotFound,
                        Detail = $"error occur because the request{context.Request.Path}",
                        Instance = context.Request.Path
                    };
                await context.Response.WriteAsJsonAsync(problem);// Convert form string(problem) to json 
                    
                }

            }
            catch(Exception ex)
            {
                //logging instead of console.writeline becuase it make more details

                _logger.LogError(ex, "something went wrong");
                //return custom error responed  [handle respone of request to help front to understand]

                var problem = new ProblemDetails()
                {
                    Title = "Error while processing Http Request",
                    Detail = ex.Message,
                    Instance = context.Request.Path,// tell about the path that make error
                    Status = ex switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,

                        _ => StatusCodes.Status500InternalServerError
                    }


                };
                context.Response.StatusCode = problem.Status.Value;

                await context.Response.WriteAsJsonAsync(problem);// Convert form string(problem) to json 

            }
        }
    }
}
