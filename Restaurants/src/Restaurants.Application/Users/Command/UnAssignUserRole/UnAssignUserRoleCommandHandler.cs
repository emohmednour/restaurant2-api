using MediatR;
using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Command.UnAssignUserRole;

public class UnAssignUserRoleCommandHandler(
    RoleManager<IdentityRole> roleManager,
    UserManager<User> userManager) : IRequestHandler<UnAssignUserRoleCommand>
{
    public async Task Handle(UnAssignUserRoleCommand request, CancellationToken cancellationToken)
    {

        var user = await userManager.FindByNameAsync(request.UserEmail)
            ?? throw new NotFoundException(nameof(User), request.UserEmail);

        var role = await roleManager.FindByNameAsync(request.RoleName)
            ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);


        await userManager.RemoveFromRoleAsync(user, request.RoleName);
    }
}
