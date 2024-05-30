using Bogus;
using Identity.UnitTests.Extensions;
using Identity.Services.Dtos.RequestDtos;
using Identity.Domain.Enums;

namespace Identity.UnitTests.Helpers.BogusFaker
{
    internal class RequestRegistrationUserDtoFakerHelper
    {
        private Faker<RequestRegistrationUserDto> GenerateFakeRRequestRegistrationUserDto()
        {
            return new Faker<RequestRegistrationUserDto>()
                .RuleFor(requestLoginUserDto => requestLoginUserDto.Email,
                    faker => faker.Person.Email)
                .RuleFor(requestLoginUserDto => requestLoginUserDto.Password,
                    faker => faker.Internet.PasswordCustom())
                .RuleFor(requestLoginUserDto => requestLoginUserDto.PhoneNumber,
                    faker => faker.Phone.PhoneNumber("+375#########"))
                .RuleFor(requestLoginUserDto => requestLoginUserDto.FirstName,
                    faker => faker.Person.FirstName)
                .RuleFor(requestLoginUserDto => requestLoginUserDto.MiddleName,
                    faker => faker.Person.FirstName)
                .RuleFor(requestLoginUserDto => requestLoginUserDto.LastName,
                    faker => faker.Person.LastName)
                .RuleFor(requestLoginUserDto => requestLoginUserDto.DateOfBirth,
                    faker => faker.Person.DateOfBirth)
                .RuleFor(requestLoginUserDto => requestLoginUserDto.Sex,
                    faker => faker.Random.Enum<Sex>().ToString());
        }

        public RequestRegistrationUserDto GenerateFakeOneValidRequestRegistrationUserDto()
        {
            return GenerateFakeRRequestRegistrationUserDto()
                .Generate();
        }
    }
}
