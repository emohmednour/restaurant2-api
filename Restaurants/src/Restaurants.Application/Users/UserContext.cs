using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Restaurants.Application.Users;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{

    public CurrentUser? GetCurrentUser()
    {
        var user = httpContextAccessor?.HttpContext?.User;

        if (user == null)
        {
            throw new InvalidOperationException("User context is not present");
        }

        if (user.Identity == null || !user.Identity.IsAuthenticated)
        {
            return null;
        }


        var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
        var role = user.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value);


        var nationality = user.FindFirst(x => x.Type == "Nationality")?.Value;
        var dataofbirth = user.FindFirst(x => x.Type == "DateOfBirth")?.Value;

        var dateOfBirth =
            dataofbirth == null ? (DateOnly?)null : DateOnly.ParseExact(dataofbirth, "yyyy-MM-dd");


        return new CurrentUser(userId, email, role,nationality,dateOfBirth);
    }

}

public interface IUserContext
{
    public CurrentUser? GetCurrentUser();
}