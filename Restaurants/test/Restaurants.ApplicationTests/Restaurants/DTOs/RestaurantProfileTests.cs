using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;
using Xunit;

namespace Restaurants.Application.Restaurants.DTOs.Tests
{
    
    public class RestaurantProfileTests
    {
        private  IMapper _mapper;
        public RestaurantProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RestaurantProfile>();
            },NullLoggerFactory.Instance);


            _mapper = configuration.CreateMapper();
        }

        [Fact()]
        public void Mapping_Restaurant_to_RestaurantDto_MapsCorrectly()
        {
            var restaurant = new Restaurant
            {
                Id = 1,
                Name = "Test restaurant",
                Description = "Test Description",
                Category = "Test Category",
                HasDelivery = true,
                ContactEmail = "test@example.com",
                ContactNumber = "123456789",
                Address = new Address
                {
                    City = "Test City",
                    Street = "Test Street",
                    PostalCode = "12-345"
                }

            };


            //act
            var restaurantDto = _mapper.Map<RestaurantDTO>(restaurant);

            //assert
            restaurantDto.Should().NotBeNull();
            restaurantDto.Name.Should().Be("Test restaurant");
            restaurantDto.Description.Should().Be("Test Description");
            restaurantDto.Category.Should().Be("Test Category");
            restaurantDto.HasDelivery.Should().Be(true);
            restaurantDto.City.Should().Be("Test City");
            restaurantDto.Street.Should().Be("Test Street");
            restaurantDto.PostalCode.Should().Be("12-345");


        }



        [Fact()]
        public void Mapping_CreateRestaurantCommand_to_Restaurant_MapsCorrectly() 
        {

            //arrange
            var command = new CreateRestaurantCommand
            {
                Name = "Test Restaurant",
                Description = "Test Description",
                Category = "Test Category",
                HasDelivery = true,
                
                City = "Test City",
                Street = "Test Street",
                PostalCode = "12345"



            };

            //act
            var restaurant = _mapper.Map<Restaurant>(command);


            restaurant.Should().NotBeNull();
            restaurant.Name.Should().Be(command.Name);
            restaurant.Description.Should().Be(command.Description);
            restaurant.Category.Should().Be(command.Category);
            restaurant.HasDelivery.Should().Be(true);


            restaurant.Address.Should().NotBeNull();
            restaurant.Address.City.Should().Be(command.City);

            restaurant.Address.Street.Should().Be(command.Street);
            restaurant.Address.PostalCode.Should().Be(command.PostalCode);

        }


        [Fact()]
        public void Mapping_UpdateRestaurantCommand_to_Restaurant_MapsCorrectly() {


            int Id = 4;
            //arrange
            var command = new UpdateRestaurantCommand
            {
                Id = Id,
                Name = "Test restaurant",
                Description = "Test Description",
                HasDelivery = true,




            };

            //act
            var restaurant = _mapper.Map<Restaurant>(command);


            restaurant.Should().NotBeNull();
            restaurant.Id.Should().Be(command.Id);
            restaurant.Name.Should().Be(command.Name);
            restaurant.Description.Should().Be("Test Description");
            
            restaurant.HasDelivery.Should().Be(true);


           

        }
    }
}