using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Constants;


namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurant;
public  class GetAllRestaurantQuery : IRequest<PageResult<RestaurantDTO>>
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public string? Sortby { get; set; }
    public SortDirection SortDirection { get; set; }
}
