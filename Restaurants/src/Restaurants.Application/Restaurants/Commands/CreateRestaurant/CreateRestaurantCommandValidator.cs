using FluentValidation;


namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{

    // أفضل تكون static readonly لزيادة الأداء
    private readonly static List<string> ValidCategory  = ["Italian", "Mexican", "Japanese", "American", "Indian"];

    public CreateRestaurantCommandValidator()
    {
        RuleFor(x => x.Name)
            .Length(3, 50);
            //.NotEmpty();

        //RuleFor(x => x.Description)
        //    .NotEmpty()
        //    .WithMessage("description required");

        RuleFor(x => x.Category)
            .Must (ValidCategory.Contains)
            .WithMessage($"Category must be in{string.Join(',',ValidCategory)}");

        RuleFor(x => x.PostalCode)
            .Matches(@"^\d{2}-\d{3}").
            WithMessage("ex XX-XXX");
    }


}
