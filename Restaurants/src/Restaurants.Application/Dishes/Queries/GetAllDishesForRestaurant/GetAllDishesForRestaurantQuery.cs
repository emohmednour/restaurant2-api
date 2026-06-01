using MediatR;
using Restaurants.Application.Dishes.DTOs;

namespace Restaurants.Application.Dishes.Queries.GetAllDishesForRestaurant;

public class GetAllDishesForRestaurantQuery(int restaurantid) : IRequest<IEnumerable<DishDto>>
{
    public int Restaurantid { get; } = restaurantid;
}
