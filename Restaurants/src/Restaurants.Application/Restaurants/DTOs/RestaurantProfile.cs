using AutoMapper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.DTOs;

public class RestaurantProfile : Profile
{


    public  RestaurantProfile()
    {

        CreateMap<Restaurant, RestaurantDTO>()
            .ForMember(cfg => cfg.City,
            option => option.MapFrom(x => x.Address == null ? null : x.Address.City))
            .ForMember(cfg => cfg.PostalCode,
            option => option.MapFrom(x => x.Address == null ? null : x.Address.PostalCode))
            .ForMember(cfg => cfg.Street,
            option => option.MapFrom(x => x.Address == null ? null : x.Address.Street))
            .ForMember(cfg=>cfg.Dishes,src => src.MapFrom(x=>x.Dishes));

        CreateMap<CreateRestaurantCommand, Restaurant>()
            .ForMember(c => c.Address, x => x.MapFrom(
                src => new Address
                {
                    City = src.City,
                    PostalCode = src.PostalCode,
                    Street = src.Street

                }
                ));





        CreateMap<UpdateRestaurantCommand, Restaurant>();

    }
}
