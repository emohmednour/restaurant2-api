using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteDishForRestaurant;

public class DeleteDishesForRestaurantCommand(int restaurantId) : IRequest
{
    public int RestaurantId { get; } = restaurantId;
}
