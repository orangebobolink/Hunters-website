using FluentValidation;
using Modules.Animal.Application.Dtos.RequestDtos;

namespace Modules.Animal.API.Validators
{
    public class HuntingSeasonRequestDtoValidator
         : AbstractValidator<HuntingSeasonRequestDto>
    {
        public HuntingSeasonRequestDtoValidator()
        {
            RuleFor(season => season.StartDate)
                .NotEmpty()
                .WithMessage("Start date is required.")
                .LessThan(season => season.EndDate)
                .WithMessage("Start date must be before the end date.");

            RuleFor(season => season.EndDate)
                .NotEmpty()
                .WithMessage("End date is required.")
                .GreaterThan(season => season.StartDate)
                .WithMessage("End date must be after the start date.");

            RuleFor(season => season.WayOfHunting)
                .NotEmpty()
                .WithMessage("Way of hunting is required.")
                .MaximumLength(100)
                .WithMessage("Way of hunting must not exceed 100 characters.");
        }
    }
}
