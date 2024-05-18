using FluentValidation.TestHelper;
using Identity.Domain.Helpers;
using Identity.Services.Dtos.RequestDtos;
using Identity.Services.Validators;
using Identity.UnitTests.Data.BogusData;

namespace Identity.UnitTests.Tests.ValidatorsTests
{
    public class RequestLoginUserDtoValidatorTests
    {
        private readonly RequestLoginUserDtoValidator _requestLoginUserDtoValidator = new();
        private readonly RequestLoginUserDtoBogusData _requestLoginUserDtoBogusData = new();

        [Fact]
        public void RequestLoginUserDtoValidator_ShouldPassValidation_WhenDtoIsValid()
        {
            // Arrange
            var dto = _requestLoginUserDtoBogusData
                .GenerateFakeOneValidRequestLoginUserDto();

            // Act
            var result = _requestLoginUserDtoValidator
                .TestValidate(dto);

            // Assert
            result
                .ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData("", "password123", UserErrorHelper.EmptyEmailError)]
        [InlineData("invalidemail", "password123", UserErrorHelper.InvalidEmailError)]
        public void RequestLoginUserDtoValidator_ShouldHaveError_InvalidEmail(
            string email,
            string password,
            string errorMessage)
        {
            // Arrange
            var dto = new RequestLoginUserDto
            {
                Email = email,
                Password = password
            };

            // Act
            var result = _requestLoginUserDtoValidator
                .TestValidate(dto);

            // Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage(errorMessage);
        }

        [Theory]
        [InlineData("email@email.com", "", UserErrorHelper.EmptyPasswordError)]
        public void RequestLoginUserDtoValidator_ShouldHaveError_InvalidPassword(
            string email,
            string password,
            string errorMessage)
        {
            // Arrange
            var dto = new RequestLoginUserDto
            {
                Email = email,
                Password = password
            };

            // Act
            var result = _requestLoginUserDtoValidator
                .TestValidate(dto);

            // Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage(errorMessage);
        }
    }
}
