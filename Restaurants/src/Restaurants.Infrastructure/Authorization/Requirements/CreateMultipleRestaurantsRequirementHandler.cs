using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements;

internal class CreateMultipleRestaurantsRequirementHandler
    (IUserContext userContext,
    IRestaurantRepository restaurantRepository) : AuthorizationHandler<CreateMultipleRestaurantsRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreateMultipleRestaurantsRequirement requirement)
    {
        var user = userContext.GetCurrentUser();


        var rests =await restaurantRepository.GetAllAsync();

        var count  = rests.Count(x=>x.OwnerId == user.Id);

        if (count >= requirement.MinimumCreatedRestaurants) {

            context.Succeed(requirement);
            
        
        }
        else
        {
            context.Fail();
        }

    

       
    }
}
