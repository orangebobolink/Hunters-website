using FluentValidation;
using Identity.Domain.Helpers;
using Identity.Services.Dtos.RequestDtos;

namespace Identity.Services.Validators
{
    public class RequestUserDtoValidator
        : AbstractValidator<RequestUserDto>
    {
        public RequestUserDtoValidator()
        {
            RuleFor(user => user.UserName)
                .NotEmpty()
                .WithMessage(UserErrorHelper.EmptyUsernameError)
                .MinimumLength(4)
                .MaximumLength(20)
                .WithMessage(UserErrorHelper.InvalidUsernameError);

            RuleFor(user => user.Email)
                .NotEmpty()
                .WithMessage(UserErrorHelper.EmptyEmailError)
                .EmailAddress()
                .WithMessage(UserErrorHelper.InvalidEmailError);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(UserErrorHelper.EmptyPasswordError)
                .MinimumLength(8)
                .WithMessage(UserErrorHelper.InvalidMinimumLengthPasswordError)
                .Matches(UserMatchHelper.PasswordMatch)
                .WithMessage(UserErrorHelper.InvalidPasswordError);

            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage(UserErrorHelper.EmptyPhoneError)
                .Matches(UserMatchHelper.PhoneMatch)
                .WithMessage(UserErrorHelper.InvalidPhoneError);
        }
    }
}
