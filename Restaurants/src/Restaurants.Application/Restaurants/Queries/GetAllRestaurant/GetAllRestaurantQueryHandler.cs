using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Repositories;


namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurant;

public class GetAllRestaurantQueryHandler
    (ILogger<GetAllRestaurantQueryHandler> logger,
    IMapper mapper,IRestaurantRepository restaurantRepository)
    : IRequestHandler<GetAllRestaurantQuery, PageResult<RestaurantDTO>>
{
    public async Task<PageResult<RestaurantDTO>> Handle
        (GetAllRestaurantQuery request, CancellationToken cancellationToken)
    {

        logger.LogInformation("Getting All Restaurants");

        var (restaurants,totalcount) = await restaurantRepository.GetAllMatchingAsync
            (request.SearchPhrase,request.PageSize,request.PageNumber,request.Sortby,request.SortDirection);

        // map

        var restaurantsDto = mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);

        var result = new PageResult<RestaurantDTO>
            (restaurantsDto, totalcount, request.PageSize, request.PageNumber);


        return result;
        
    }
}
