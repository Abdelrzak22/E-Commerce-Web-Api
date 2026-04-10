using E_Commerce.ServiceAbstraction;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Attribute
{
    public class RedisCacheAttribute:ActionFilterAttribute
    {
        private readonly int _duration;

        public RedisCacheAttribute(int durarion=5)
        {
            _duration = durarion;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var cacheservice = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var cachekey = CreateCacheKey(context.HttpContext.Request);
            var cahevalue = await cacheservice.GetAsync(cachekey);
            if (cahevalue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = cahevalue,
                    ContentType = "application/Json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            var contextExcuted = await next.Invoke();
            if(contextExcuted.Result is OkObjectResult result)
            {
                await cacheservice.SetAsync(cachekey, result.Value!, TimeSpan.FromMinutes(_duration));
            }

            //return base.OnActionExecutionAsync(context, next);
        }
        private string CreateCacheKey(HttpRequest request)
        {
            StringBuilder key = new StringBuilder();
            key.Append(request.Path);//Api/products

            foreach (var item in request.Query.OrderBy( p=> p.Key))
            {
                key.Append($"|{item.Key}-{item.Value}"); //Api/products|brandid-2
            }
            return key.ToString();
        }
    }
}
