
using System.Diagnostics;

namespace Restaurants.Api.Middlewares
{
    public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var Timer  = Stopwatch.StartNew();

            await next.Invoke(context);

            Timer.Stop();

            if (Timer.ElapsedMilliseconds /1000 > 4 )
            {
                logger.LogInformation("Request {Method}  {Path}  {time}ms",
                    context.Request.Method,
                    context.Request.Path,
                    Timer.ElapsedMilliseconds
                    
                    );
            }
        }
    }
}
