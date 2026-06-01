using Microsoft.AspNetCore.Authorization;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Xunit;
using FluentAssertions;

namespace Restaurants.Infrastructure.Authorization.Requirements.Tests
{
    public class CreateMultipleRestaurantsRequirementHandlerTests
    {
        [Fact()]
        public async Task HandleRequirementAsync_UserHasCreatedMultipleRestaurants_ShouldSucceed() {

            //arrange 
            var usercontext = new Mock<IUserContext>();

            var user = new CurrentUser( "1", "eee@test.com", [], null, null);
            usercontext.Setup(x => x.GetCurrentUser()).Returns(user);


            var restaurants = new List<Restaurant>()
            {

                new()
                {
                    OwnerId = user.Id,
                },
                new()
                {
                    OwnerId = user.Id,
                },
                new()
                {
                    OwnerId = "2",
                },
            };


            var restaurantRepo = new Mock<IRestaurantRepository>();
            restaurantRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(restaurants);


            var requirement = new CreateMultipleRestaurantsRequirement(2);
            var handler  = new CreateMultipleRestaurantsRequirementHandler(usercontext.Object,restaurantRepo.Object);

            var context = new AuthorizationHandlerContext([requirement],null,null);

            //act
            await handler.HandleAsync(context);



            //assert
            context.HasSucceeded.Should().BeTrue();

        }
        [Fact()]
        public async Task HandleRequirementAsync_UserHasNotCreatedMultipleRestaurants_ShouldFaild() {

            //arrange 
            var usercontext = new Mock<IUserContext>();

            var user = new CurrentUser( "1", "eee@test.com", [], null, null);
            usercontext.Setup(x => x.GetCurrentUser()).Returns(user);


            var restaurants = new List<Restaurant>()
            {

                new()
                {
                    OwnerId = user.Id,
                },
               
                new()
                {
                    OwnerId = "2",
                },
            };


            var restaurantRepo = new Mock<IRestaurantRepository>();
            restaurantRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(restaurants);


            var requirement = new CreateMultipleRestaurantsRequirement(2);
            var handler  = new CreateMultipleRestaurantsRequirementHandler(usercontext.Object,restaurantRepo.Object);

            var context = new AuthorizationHandlerContext([requirement],null,null);

            //act
            await handler.HandleAsync(context);



            //assert
            context.HasFailed.Should().BeTrue();
            context.HasSucceeded.Should().BeFalse();

        }
    }
}