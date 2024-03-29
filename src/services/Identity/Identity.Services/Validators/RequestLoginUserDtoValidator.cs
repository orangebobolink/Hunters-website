using FluentValidation;
using Identity.Domain.Helpers;
using Identity.Services.Dtos.RequestDtos;

namespace Identity.Services.Validators
{
    public class RequestLoginUserDtoValidator
         : AbstractValidator<RequestLoginUserDto>
    {
        public RequestLoginUserDtoValidator()
        {
            RuleFor(user => user.Email)
               .NotEmpty()
               .WithMessage(UserErrorHelper.EmptyEmailError)
               .EmailAddress()
               .WithMessage(UserErrorHelper.InvalidEmailError);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(UserErrorHelper.EmptyPasswordError);
        }
    }
}
