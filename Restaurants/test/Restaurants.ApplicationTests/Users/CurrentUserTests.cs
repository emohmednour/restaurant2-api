using Restaurants.Domain.Constants;
using Xunit;
using FluentAssertions;
namespace Restaurants.Application.Users.Tests
{
    public class CurrentUserTests
    {
        [Fact()]
        public void IsInRole_WithMatchingRole_ShouldReturnTrue()
        {
            //Arrange
            var user = new CurrentUser("1","text@Gmail.com",[UserRoles.Admin],null,null);


            //act
           var isinrole = user.IsInRole(UserRoles.Admin);

            //assert
            isinrole.Should().BeTrue();
           
        }
        [Fact()]
        public void IsInRole_WithMatchingRole_ShouldReturnFalse()
        {
            //Arrange
            var user = new CurrentUser("1","text@Gmail.com",[UserRoles.Admin],null,null);


            //act
           var isinrole = user.IsInRole(UserRoles.Owner);

            //assert
            isinrole.Should().BeFalse();
           
        }
        [Fact()]
        public void IsInRole_WithDifferentCase_ShouldReturnFalse()
        {

            //Arrange
            var user = new CurrentUser("1", "text@Gmail.com", ["admin"], null, null);


            //act
            var isinrole = user.IsInRole(UserRoles.Admin);

            //assert
            isinrole.Should().BeFalse();



        }
        [Theory]
        [InlineData(UserRoles.Admin)]
        [InlineData(UserRoles.Owner)]
        [InlineData(UserRoles.User)]
        public void IsInRole_WithMultipleRole_ShouldReturnTrue(string RoleName)
        {
            //Arrange
            var user = new CurrentUser("1", "text@Gmail.com", [RoleName], null, null);


            //act
            var isinrole = user.IsInRole(RoleName);

            //assert
            isinrole.Should().BeTrue();

        }

    }
}