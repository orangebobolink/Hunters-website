using FluentValidation;
using Identity.Services.Dtos;

namespace Identity.Services.Validators
{
    public class TokenApiDtoValidator
         : AbstractValidator<TokenApiDto>
    {
        public TokenApiDtoValidator()
        {
            RuleFor(user => user.AccessToken)
               .NotEmpty()
               .WithMessage("Access token is empty");

            RuleFor(user => user.RefreshToken)
               .NotEmpty()
               .WithMessage("Refresh token is empty");
        }
    }
}
