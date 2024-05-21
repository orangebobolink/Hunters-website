using FluentValidation.TestHelper;
using Identity.Domain.Helpers;
using Identity.Services.Validators;
using Identity.UnitTests.Helpers.BogusFaker;

namespace Identity.UnitTests.Tests.ValidatorTests
{
    public class RequestRegistrationUserDtoValidatorTest
    {
        private readonly RequestRegistrationUserDtoFakerHelper _faker = new();
        private readonly RequestRegistrationUserDtoValidator _validator = new();

        [Fact]
        public void RequestRegistrationUserDtoValidator_ShouldPassValidation_WhenDtoIsValid()
        {
            // Arrange
            var dto = _faker
                .GenerateFakeOneValidRequestRegistrationUserDto();

            // Act
            var result = _validator
                .TestValidate(dto);

            // Assert
            result
                .ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData("", UserErrorHelper.EmptyEmailError)]
        [InlineData("invalidemail", UserErrorHelper.InvalidEmailError)]
        public void RequestRegistrationUserDtoValidator_ShouldHaveError_InvalidEmail(
            string email,
            string errorMessage)
        {
            // Arrange
            var dto = _faker
                .GenerateFakeOneValidRequestRegistrationUserDto() with
            {
                Email = email
            };

            // Act
            var result = _validator
                .TestValidate(dto);

            // Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage(errorMessage);
        }

        [Theory]
        [InlineData("", UserErrorHelper.EmptyPasswordError)]
        [InlineData("short", UserErrorHelper.InvalidMinimumLengthPasswordError)]
        [InlineData("NOLOWERCASE1!", UserErrorHelper.InvalidPasswordError)]
        [InlineData("NoNumber!", UserErrorHelper.InvalidPasswordError)]
        [InlineData("NoSpecialChar1", UserErrorHelper.InvalidPasswordError)]
        public void RequestRegistrationUserDtoValidator_ShouldHaveError_InvalidPassword(
            string password,
            string errorMessage)
        {
            // Arrange
            var dto = _faker
                .GenerateFakeOneValidRequestRegistrationUserDto() with
            {
                Password = password
            };

            // Act
            var result = _validator
                .TestValidate(dto);

            // Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage(errorMessage);
        }


        [Theory]
        [InlineData("", UserErrorHelper.EmptyPhoneError)]
        [InlineData("short", UserErrorHelper.InvalidPhoneError)]
        [InlineData("+375-3222222", UserErrorHelper.InvalidPhoneError)]
        [InlineData("+23", UserErrorHelper.InvalidPhoneError)]
        public void RequestRegistrationUserDtoValidator_ShouldHaveError_InvalidPhoneNumber(
            string phoneNumber,
            string errorMessage)
        {
            // Arrange
            var dto = _faker
                .GenerateFakeOneValidRequestRegistrationUserDto() with
            {
                PhoneNumber = phoneNumber
            };

            // Act
            var result = _validator
                .TestValidate(dto);

            // Assert
            result
                .ShouldHaveValidationErrorFor(x => x.PhoneNumber)
                .WithErrorMessage(errorMessage);
        }
    }
}
