using MediatR;

namespace Restaurants.Application.Users.Command.UpdateUserDetails;

public class UpdateUserDetailsCommand : IRequest
{
    public string? Nationality { get; set; }
    public DateOnly? DateOfBirth { get; set; }
}
