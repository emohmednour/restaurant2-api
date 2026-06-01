using AutoMapper;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.DTOs
{
    public class DishProfile : Profile
    {

        public DishProfile() {


            CreateMap<Dish, DishDto>();
            CreateMap<CreateDishCommand,Dish>();
        }
    }
}
