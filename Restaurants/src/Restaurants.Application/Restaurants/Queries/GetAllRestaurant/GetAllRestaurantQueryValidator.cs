using FluentValidation;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurant;

public class GetAllRestaurantQueryValidator : AbstractValidator<GetAllRestaurantQuery>
{

    private readonly static List<int> AllowedPageSizes = [5,10,15,20];
    private string[] AllowedSortedby = 
        [nameof(RestaurantDTO.Name), nameof(RestaurantDTO.Category ), nameof(RestaurantDTO.Description)];

    public GetAllRestaurantQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .Must(x => AllowedPageSizes.Contains(x))
            .WithMessage($"PageNumber must be in{string.Join(',', AllowedPageSizes)}");
        RuleFor(x => x.Sortby)
            .Must(x => AllowedSortedby.Contains(x))
            .When(x=>x.Sortby != null)
            .WithMessage($"PageNumber must be in{string.Join(',', AllowedSortedby)}");

    }
}
