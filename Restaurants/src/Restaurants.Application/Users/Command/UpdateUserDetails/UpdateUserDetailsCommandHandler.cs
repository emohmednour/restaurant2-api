using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
namespace Restaurants.Application.Users.Command.UpdateUserDetails;

public class UpdateUserDetailsCommandHandler(
    IUserContext userContext,
    ILogger<UpdateUserDetailsCommandHandler>logger,
    IUserStore<User> userStore
    ) : IRequestHandler<UpdateUserDetailsCommand>
{
    public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        logger.LogInformation("update for user id : {id}, with {@request}",user!.Id,request );

        var userDb =await userStore.FindByIdAsync(user!.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(User), user.Id);

        userDb.DateOfBirth = request.DateOfBirth;
        userDb.Nationality = request.Nationality;

        await userStore.UpdateAsync(userDb,cancellationToken);


        


    }
}
