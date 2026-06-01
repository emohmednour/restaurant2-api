using Microsoft.OpenApi.Models;
using Restaurants.Api.Middlewares;
using Serilog;

namespace Restaurants.Api.Extensions
{
    public static class WebApplicationsBuilderExtensions
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication();
            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen(c =>
            {

                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {

                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",

                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                          Reference = new OpenApiReference{
                           Type = ReferenceType.SecurityScheme,
                           Id = "bearerAuth"

                          }
                        },[]

                    }
                });

            });


            builder.Services.AddScoped<ErrorHandlingMiddleware>();
            builder.Services.AddScoped<RequestTimeLoggingMiddleware>();
            builder.Services.AddEndpointsApiExplorer();

            builder.Host.UseSerilog((context, confg) =>
            {

                confg.ReadFrom.Configuration(context.Configuration);
            });
        }
    }
}
