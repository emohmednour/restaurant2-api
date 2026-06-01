using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
    IMapper mapper
    , IRestaurantRepository restaurantRepository, 
    IRestaurantAuthorizationService authorizationService,
    IDishRepository dishRepository)
    : IRequestHandler<CreateDishCommand, int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("create a dish  {@dish} ", request);

        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId)
            ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        if (!authorizationService.Authorize(restaurant, ResourceOperation.Update))
        {
            throw new ForbidException();
        }
        var dish = mapper.Map<Dish>(request);
       return  await dishRepository.Create(dish);
       

    }
}
