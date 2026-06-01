using FluentValidation;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
{
    public CreateDishCommandValidator() {

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price must be a non-negative number.")
            ;


        RuleFor(dish => dish.KiloCalories)
             .GreaterThanOrEqualTo(0)
             .When(x=>x.KiloCalories.HasValue)
             .WithMessage("KiloCalories must be a non-negative number.");

    }
}
