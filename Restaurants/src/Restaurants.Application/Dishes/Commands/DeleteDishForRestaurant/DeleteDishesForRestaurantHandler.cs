using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDishForRestaurant;

public class DeleteDishesForRestaurantHandler(ILogger<CreateDishCommandHandler> logger
    , IRestaurantRepository restaurantRepository,
    IRestaurantAuthorizationService authorizationService,
    IDishRepository dishRepository) : IRequestHandler<DeleteDishesForRestaurantCommand>
{
    public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting  Dishes for id :{@restaurantid}",
            request.RestaurantId);

        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId)
           ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        if (!authorizationService.Authorize(restaurant,ResourceOperation.Update))
        {
            throw new ForbidException();
        }
        await dishRepository.Delete(restaurant.Dishes);



    }
}
