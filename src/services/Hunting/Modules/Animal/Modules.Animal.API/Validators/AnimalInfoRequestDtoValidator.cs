using FluentValidation;
using Modules.Animal.Application.Dtos.RequestDtos;

namespace Modules.Animal.API.Validators
{
    public class AnimalInfoRequestDtoValidator
           : AbstractValidator<AnimalInfoRequestDto>
    {
        public AnimalInfoRequestDtoValidator()
        {
            RuleFor(animalInfo => animalInfo.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name must not exceed 100 characters.");

            RuleFor(animalInfo => animalInfo.Description)
                .MaximumLength(1000)
                .WithMessage("Description must not exceed 1000 characters.");
        }
    }
}
