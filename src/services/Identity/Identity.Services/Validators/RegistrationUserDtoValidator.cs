﻿using FluentValidation;
using Identity.Domain.Helpers;
using Identity.Services.Dtos;

namespace Identity.Services.Validators
{
    internal class RegistrationUserDtoValidator
        : AbstractValidator<RegistrationUserDto>
    {
        public RegistrationUserDtoValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty()
                .WithMessage(UserErrorHelper.EmptyEmailError)
                .EmailAddress()
                .WithMessage(UserErrorHelper.InvalidEmailError);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(UserErrorHelper.EmptyPasswordError)
                .MinimumLength(8)
                .WithMessage(UserErrorHelper.InvalidMinimumLenghtPasswordError)
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
