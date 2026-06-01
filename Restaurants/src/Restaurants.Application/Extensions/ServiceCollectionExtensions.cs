using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Users;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var Assembles = typeof(ServiceCollectionExtensions).Assembly;


        services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssembly(Assembles));


        services.AddAutoMapper(cfg =>
        cfg.AddMaps(Assembles));




        services.AddValidatorsFromAssembly(Assembles)
            .AddFluentValidationAutoValidation();



        services.AddScoped<IUserContext, UserContext>();
        services.AddHttpContextAccessor();


    }
}
