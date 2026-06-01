using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Requirements;
using Restaurants.Infrastructure.Authorization.Services;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repository;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extensions;

public static class ServicesCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var config = configuration.GetConnectionString("RestaurantDb");
        services.AddDbContext<RestaurantsDbContext>(option =>
            option.UseSqlServer(config)
                  .EnableSensitiveDataLogging());

        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipleFactory>()
            .AddEntityFrameworkStores<RestaurantsDbContext>();

        services.AddScoped<IRestaurantSeeders, RestaurantSeeders>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IDishRepository, DishRepository>();



        services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
        services.AddScoped<IAuthorizationHandler, CreateMultipleRestaurantsRequirementHandler>();

        services.AddAuthorizationBuilder().
            AddPolicy(AppClaimType.Nationality,
            policy =>
            {
                policy.RequireClaim(PolicyNames.HasNationality, "Polish", "Italian");
            }
            )
            .AddPolicy(PolicyNames.AtLeast20,
            policy =>
            {
                policy.AddRequirements(new MinimumAgeRequirement(20));
            }
            )
            .AddPolicy(PolicyNames.AtLeast2Restaurant,
            policy =>
            {
                policy.AddRequirements(new CreateMultipleRestaurantsRequirement(2));
            })
            
            ;



        services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();

    }
}