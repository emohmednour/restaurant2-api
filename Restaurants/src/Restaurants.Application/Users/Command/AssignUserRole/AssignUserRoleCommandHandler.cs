using MediatR;
using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Command.AssignUserRole;

public class AssignUserRoleCommandHandler(
    RoleManager<IdentityRole> roleManager,
    UserManager<User> userManager) : IRequestHandler<AssignUserRoleCommand>
{
    public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        

        var user = await userManager.FindByNameAsync(request.UserEmail)
            ?? throw new NotFoundException(nameof(User),request.UserEmail);

        var role =await roleManager.FindByNameAsync(request.RoleName)
            ?? throw new NotFoundException(nameof(IdentityRole),request.RoleName);


        await userManager.AddToRoleAsync(user, request.RoleName);

      
    }
}
