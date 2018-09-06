using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CoreTestApp
{
    public class RouterMiddleware
    {
        private readonly RequestDelegate _next;

        public RouterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string path = context.Request.Path.Value.ToLower();

            if (path == "/index")
            {
                await context.Response.WriteAsync("Home Page");
            }
            else if (path == "/about")
            {
                await context.Response.WriteAsync("About");
            }
            else
            {
                context.Response.StatusCode = 404;
            }

            await _next.Invoke(context);
        }
    }
}
