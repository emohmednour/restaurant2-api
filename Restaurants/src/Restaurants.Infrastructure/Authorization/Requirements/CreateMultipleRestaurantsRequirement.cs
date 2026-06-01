using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirements;

public class CreateMultipleRestaurantsRequirement(int minimumCreatedRestaurants)  :IAuthorizationRequirement
{
    public int MinimumCreatedRestaurants { get; set; } = minimumCreatedRestaurants;
}
