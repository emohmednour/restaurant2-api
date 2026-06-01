

using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_forValidCreateRestaurant_ShouldReturnId()
        {
            var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();
            var mapperMock = new Mock<IMapper>();
            var UserContext = new Mock<IUserContext>();
            var RestaurantRepo = new Mock<IRestaurantRepository>();

            var command = new CreateRestaurantCommand();
            var restaurant = new Restaurant();
            mapperMock.Setup(x => x.Map<Restaurant>(command)).Returns(restaurant);


            var user = new CurrentUser("1", "eE2@gmail.com", [],null,null);
            UserContext.Setup(x => x.GetCurrentUser()).Returns(user);

            RestaurantRepo.Setup(x => x.Create(It.IsAny<Restaurant>())).ReturnsAsync(1);

            var commandhandler = new CreateRestaurantCommandHandler
                (loggerMock.Object,mapperMock.Object,RestaurantRepo.Object, UserContext.Object);

            //act 
            var result  = await commandhandler.Handle(command,CancellationToken.None);


            //assert
            result.Should().Be(1);

            restaurant.OwnerId.Should().Be(user.Id);

            RestaurantRepo.Verify(x => x.Create(restaurant), Times.Once);









            

        }
       
    
    }
}