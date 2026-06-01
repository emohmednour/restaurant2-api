
using Restaurants.Domain.Exceptions;

namespace Restaurants.Api.Middlewares
{
    public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {

                await next.Invoke(context);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex.Message);

                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(ex.Message);

            }
            catch (ForbidException ex)
            {
                logger.LogWarning(ex.Message);

                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Access Forbden");

            }
            catch (Exception ex)
            {

                logger.LogError(ex, "Something went wrong");

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
