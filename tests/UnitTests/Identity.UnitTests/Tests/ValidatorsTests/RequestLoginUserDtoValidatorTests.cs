using FluentValidation.TestHelper;
using Identity.Services.Validators;
using Identity.UnitTests.Data.BogusData;

namespace Identity.UnitTests.Tests.ValidatorsTests
{
    public class RequestLoginUserDtoValidatorTests
    {
        private readonly RequestLoginUserDtoValidator _requestLoginUserDtoValidator = new();
        private readonly RequestLoginUserDtoBogusData _requestLoginUserDtoBogusData = new();

        [Fact]
        public async Task ValidateRequestLoginUserDto_WhenModelIsValid_ShouldBeSuccessfulValidation()
        {
            // Arrange
            var requestLoginUserDto = _requestLoginUserDtoBogusData.GenerateFakeOneValidRequestLoginUserDto();

            // Act
            var result = await _requestLoginUserDtoValidator.TestValidateAsync(requestLoginUserDto);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
