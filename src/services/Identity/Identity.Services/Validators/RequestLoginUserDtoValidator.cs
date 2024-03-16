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
            RuleFor(user => user.UserName)
               .NotEmpty()
               .WithMessage(UserErrorHelper.EmptyEmailError);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(UserErrorHelper.EmptyPasswordError);
        }
    }
}
