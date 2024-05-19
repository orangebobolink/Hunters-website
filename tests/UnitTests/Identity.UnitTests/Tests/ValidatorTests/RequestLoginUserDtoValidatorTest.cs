using FluentValidation.TestHelper;
using Identity.Domain.Helpers;
using Identity.Services.Validators;
using Identity.UnitTests.Data.BogusData;

namespace Identity.UnitTests.Tests.ValidatorsTests
{
    public class RequestLoginUserDtoValidatorTest
    {
        private readonly RequestLoginUserDtoValidator _validator = new();
        private readonly RequestLoginUserDtoFaker _faker = new();

        [Fact]
        public void RequestLoginUserDtoValidator_ShouldPassValidation_WhenDtoIsValid()
        {
            // Arrange
            var dto = _faker
                .GenerateFakeOneValidRequestLoginUserDto();

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
        public void RequestLoginUserDtoValidator_ShouldHaveError_InvalidEmail(
            string email,
            string errorMessage)
        {
            // Arrange
            var dto = _faker
                .GenerateFakeOneValidRequestLoginUserDto() with
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
        public void RequestLoginUserDtoValidator_ShouldHaveError_InvalidPassword(
            string password,
            string errorMessage)
        {
            // Arrange
            var dto = _faker
                .GenerateFakeOneValidRequestLoginUserDto() with
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
    }
}
