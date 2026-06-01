using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Application.Dishes.Queries.GetAllDishesForRestaurant;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishForRestaurant;

public class GetDishForRestaurantHandler(IMapper mapper,
    IRestaurantRepository restaurantRepository,
    ILogger<GetAllDishesForRestaurantHandler> logger)
    : IRequestHandler<GetDishForRestaurantQuery, DishDto>
{
    public async Task<DishDto> Handle(GetDishForRestaurantQuery request, CancellationToken cancellationToken)
    {

        logger.LogInformation("Retrieving   Dish :Id:{@dishId} for id :{@restaurantid}",
             request.DishID,
            request.Restaurantid);

        var restaurant = await restaurantRepository.GetByIdAsync(request.Restaurantid)
            ?? throw new NotFoundException(nameof(Restaurant), request.Restaurantid.ToString());

        var dishes = restaurant.Dishes;
        var dish= dishes.FirstOrDefault(x => x.Id == request.DishID)
            ?? throw new NotFoundException(nameof(Dish), request.DishID.ToString());


        return mapper.Map<DishDto>(dish);

    }
}
