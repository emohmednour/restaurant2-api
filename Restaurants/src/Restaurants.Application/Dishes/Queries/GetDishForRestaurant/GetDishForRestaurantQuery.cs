using MediatR;
using Restaurants.Application.Dishes.DTOs;

namespace Restaurants.Application.Dishes.Queries.GetDishForRestaurant;
public class GetDishForRestaurantQuery(int restaurantid,int DishId) : IRequest<DishDto>
{
    public int Restaurantid { get; } = restaurantid;
    public int DishID { get; } = DishId;
}
