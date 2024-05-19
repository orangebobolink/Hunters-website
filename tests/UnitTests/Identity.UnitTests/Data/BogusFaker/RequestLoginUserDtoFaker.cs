using Identity.Services.Dtos.RequestDtos;
using Bogus;

namespace Identity.UnitTests.Data.BogusData
{
    public class RequestLoginUserDtoFaker
    {
        private Faker<RequestLoginUserDto> GenerateFakeRequestLoginUserDto()
        {
            return new Faker<RequestLoginUserDto>()
                .RuleFor(requestLoginUserDto => requestLoginUserDto.Email,
                    faker => faker.Person.Email)
                .RuleFor(requestLoginUserDto => requestLoginUserDto.Password,
                    faker => faker.Internet.Password());
        }

        public RequestLoginUserDto GenerateFakeOneValidRequestLoginUserDto()
        {
            return GenerateFakeRequestLoginUserDto().Generate();
        }
    }
}
