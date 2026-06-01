using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Entities;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Claims;

namespace Restaurants.Infrastructure.Authorization;

public class RestaurantsUserClaimsPrincipleFactory(
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager,
    IOptions<IdentityOptions> options) : UserClaimsPrincipalFactory<User, IdentityRole>(userManager, roleManager, options)
{


    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var identity = await GenerateClaimsAsync(user);

        if (!String.IsNullOrEmpty(user.Nationality)) {

            identity.AddClaim(new Claim("Nationality", user.Nationality));
        }
        if (user.DateOfBirth != null) {

            identity.AddClaim(new Claim("Nationality", user.DateOfBirth.Value.ToString("yyyy-MM-dd")));
        }

        return new ClaimsPrincipal(identity);
    }
}
