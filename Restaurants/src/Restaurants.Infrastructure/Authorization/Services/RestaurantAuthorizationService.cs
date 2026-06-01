using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Authorization.Services;

public class RestaurantAuthorizationService
    (IUserContext userContext,
    ILogger<RestaurantAuthorizationService> logger) : IRestaurantAuthorizationService
{
    public bool Authorize(Restaurant restaurant, ResourceOperation Operation)
    {
        var user = userContext.GetCurrentUser();
        logger.LogInformation("Authorization user Email:{email}" +
            ",to Operation {Operation} for restaurant {RestaurantName}"
            ,user.Email,
            Operation,
            restaurant.Name
         

            );

        if(Operation == ResourceOperation.Read ||
            Operation == ResourceOperation.Create)
        {
            logger.LogInformation("Read/Created is Successfully");
            return true;
        }

        if(Operation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
        {
            logger.LogInformation("Delete is Successfully");
            return true;
        }

        if((Operation == ResourceOperation.Delete || Operation == ResourceOperation.Update )
            && user.Id == restaurant.OwnerId)
        {
            logger.LogInformation("Delete/Update is Successfully");
            return true;
        }
        return false;

    }
}
