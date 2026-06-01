using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Restaurants.Domain.Constants;
using System.Security.Claims;
using Xunit;


namespace Restaurants.Application.Users.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUser_WithAuthencatedUser_ShouldReturnCurrentUser()
        {
            //arrange

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var datofbith = new DateOnly(1999 , 10 , 10);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,"1"),
                new Claim(ClaimTypes.Email,"Test@gmail.com"),
                new Claim(ClaimTypes.Role,UserRoles.Admin),
                new Claim(ClaimTypes.Role,UserRoles.User),
               
                new Claim("Nationality","Polish"),
                new Claim("DateOfBirth",datofbith.ToString("yyyy-MM-dd")),
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "test"));

            httpContextAccessorMock.Setup(x => x.HttpContext).
                Returns(new DefaultHttpContext
                {
                    User = user
                });


            //act  
            var userContext = new UserContext(httpContextAccessorMock.Object);

            var currentuser = userContext.GetCurrentUser();


            //assert
            currentuser.Should().NotBeNull();
            currentuser.Id.Should().Be("1");
            currentuser.Email.Should().Be("Test@gmail.com");
            currentuser.Roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);
            currentuser.Nationality.Should().Be("Polish");
            currentuser.DateOfBirth.Should().Be(datofbith);
            
        }

        [Fact]
        public void GetCurrentUser_WithUserContextNotPresent_ThrowsInvalidOperationException()
        {
            //arrange

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            httpContextAccessorMock.Setup(x => x.HttpContext).
               Returns((HttpContext?)null);


            //act  
            var userContext = new UserContext(httpContextAccessorMock.Object);

           Action act = ()=>userContext.GetCurrentUser();

            //arrange

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("User context is not present");

        }
    }
}