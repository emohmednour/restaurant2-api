using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(
    ILogger<CreateRestaurantCommandHandler> logger,
    IMapper mapper, IRestaurantRepository restaurantRepository
    ,IUserContext UserContext)
    : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {


        var user = UserContext.GetCurrentUser();
        logger.LogInformation("Creating restaurant :{Restaurant}", request);


        var restaurant = mapper.Map<Restaurant>(request);

        restaurant.OwnerId = user.Id;

        var rest =await  restaurantRepository.Create(restaurant);

        return rest;
    }
}
