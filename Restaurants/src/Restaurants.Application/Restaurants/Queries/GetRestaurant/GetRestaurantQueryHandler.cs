using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurant;

public class GetRestaurantQueryHandler(ILogger<GetRestaurantQueryHandler> logger,IMapper mapper,IRestaurantRepository restaurantRepository)
    : IRequestHandler<GetRestaurantQuery, RestaurantDTO>
{
    public async Task<RestaurantDTO> Handle(GetRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting Restaurant With id :{RestaurantId}", request.id);


        var restaurant = await restaurantRepository.GetByIdAsync(request.id)
        ?? throw new NotFoundException(nameof(Restaurant) , request.id.ToString());

        

        //map

        var restaurantDto = mapper.Map<RestaurantDTO>(restaurant);
      


        return restaurantDto;
    }
}
