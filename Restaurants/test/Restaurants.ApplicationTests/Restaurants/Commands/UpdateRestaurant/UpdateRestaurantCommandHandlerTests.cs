using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant.Tests
{
    public class UpdateRestaurantCommandHandlerTests
    {
        private readonly Mock<ILogger<UpdateRestaurantCommandHandler>> _loggerMock;
        private readonly Mock<IRestaurantRepository> _restaurantsRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRestaurantAuthorizationService> _restaurantAuthorizationServiceMock;

        private UpdateRestaurantCommandHandler _handle;

        public UpdateRestaurantCommandHandlerTests()
        {
            _loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
            _mapperMock = new Mock<IMapper>();
            _restaurantsRepositoryMock =new Mock<IRestaurantRepository>();
            _restaurantAuthorizationServiceMock = new Mock<IRestaurantAuthorizationService>();
            _handle = new UpdateRestaurantCommandHandler(_loggerMock.Object, _mapperMock.Object
                , _restaurantsRepositoryMock.Object, _restaurantAuthorizationServiceMock.Object); 

        }



        [Fact()]
        public async Task Handle_WithValidRequest_ShouldUpdateRestaurants()
        {
            


            int id = 1;
            var restaurant = new Restaurant()
            {
                Id = id,
                Description = "test1",
                Name = "test2",
                HasDelivery = false

            };

            var command = new UpdateRestaurantCommand() { 
            
                Id = id,Description = "test2",Name = "test", HasDelivery= false
            };




            _restaurantsRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(restaurant);
            _restaurantAuthorizationServiceMock.
                Setup(x => x.Authorize(restaurant, ResourceOperation.Update))
                .Returns(true);
            //act

            await _handle.Handle(command,CancellationToken.None);

            //assert
            _mapperMock.Verify(x => x.Map( command, restaurant),Times.Once);
            _restaurantsRepositoryMock.Verify(x => x.SaveChanges(), Times.Once);
        }


        [Fact()]
        public async Task Handle_WithInValidRequest_ShouldThrowException()
        {


            int id = 1;
            

            var command = new UpdateRestaurantCommand()
            {
                Id = id,
                Description = "test1",
                Name = "test2",
            };



            _restaurantsRepositoryMock.Setup(x => x.GetByIdAsync(id))
                .ReturnsAsync((Restaurant?) null);
           Func<Task> act= async () => await _handle.Handle(command, CancellationToken.None);


            //assert
            await act.Should().ThrowAsync<NotFoundException>();
                

        }


        [Fact()]
        public async Task Handle_WithUnauthorizedUser_ShouldThrowForbidException()
        {

            int id = 1;
            var restaurant = new Restaurant()
            {
                Id = id,
                Description = "test1",
                Name = "test2",
                HasDelivery = false

            };

            var command = new UpdateRestaurantCommand()
            {

                Id = id,
                Description = "test2",
                Name = "test",
                HasDelivery = false
            };




            _restaurantsRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(restaurant);
            _restaurantAuthorizationServiceMock.
                Setup(x => x.Authorize(restaurant, ResourceOperation.Update))
                .Returns(false);
            //act

          Func<Task> act = async()=>  await _handle.Handle(command, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<ForbidException>();

        }
    }
}