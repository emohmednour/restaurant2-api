using FluentValidation.TestHelper;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandValidatorTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            //arrange
            var command = new CreateRestaurantCommand
            {
                Name = "name",
                Category = "Italian",
                PostalCode = "12-345",
            };

            //act
            var validator = new CreateRestaurantCommandValidator();
            var result  = validator.TestValidate(command);

            // assert
            result.ShouldNotHaveAnyValidationErrors();
        }


        [Fact()]
        public void Validator_ForInValidCommand_ShouldHaveValidationErrors()
        {
            //arrange
            var command = new CreateRestaurantCommand
            {
                Name = "na",
                Category = "italian",
                PostalCode = "12345",
            };

            //act
            var validator = new CreateRestaurantCommandValidator();
            var result  = validator.TestValidate(command);

            // assert
            result.ShouldHaveValidationErrorFor(x=>x.Name);
            result.ShouldHaveValidationErrorFor(x=>x.Category);
            result.ShouldHaveValidationErrorFor(x=>x.PostalCode);
        }

        [Theory]
        [InlineData("Italian")]
        [InlineData("Mexican")]
        [InlineData("Japanese")]
        [InlineData("American")]
        [InlineData("Indian")]
        public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty
            (string category)
        {
            //arrange
            var command = new CreateRestaurantCommand
            {
               
                Category = category
               
            };

            //act
            var validator = new CreateRestaurantCommandValidator();
            var result = validator.TestValidate(command);

            // assert
            result.ShouldNotHaveValidationErrorFor(x=>x.Category);
        }
        [Theory]
        [InlineData("14236")]
        [InlineData("14-14")]
        [InlineData("147-99")]
        [InlineData("101 77")]
        [InlineData("17-3 14")]
        public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorsForPostalCodeProperty
            (string PostalCode)
        {
            //arrange
            var command = new CreateRestaurantCommand
            {
               
                PostalCode = PostalCode
               
            };

            //act
            var validator = new CreateRestaurantCommandValidator();
            var result = validator.TestValidate(command);

            // assert
            result.ShouldHaveValidationErrorFor(x=>x.PostalCode);
        }









    }
}