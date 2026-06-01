using MediatR;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurant;

public  class GetRestaurantQuery(int Id) :IRequest<RestaurantDTO>
{
    public int id { get; } = Id;
}
