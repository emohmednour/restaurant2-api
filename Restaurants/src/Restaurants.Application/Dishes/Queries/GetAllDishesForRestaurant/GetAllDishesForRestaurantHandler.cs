using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetAllDishesForRestaurant;

public class GetAllDishesForRestaurantHandler(IMapper mapper,
    IRestaurantRepository restaurantRepository,
    ILogger<GetAllDishesForRestaurantHandler> logger)
    
    : IRequestHandler<GetAllDishesForRestaurantQuery, IEnumerable<DishDto>>
{
    public async Task<IEnumerable<DishDto>> Handle(GetAllDishesForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving  all Dishes for id :{restaurantid}", request.Restaurantid);


        var restaurant = await restaurantRepository.GetByIdAsync(request.Restaurantid)
            ?? throw new NotFoundException(nameof(Restaurant), request.Restaurantid.ToString());



        return mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);
    }
}
