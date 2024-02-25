using Identity.Services.Dtos.RequestDtos;
using Bogus;

namespace Identity.UnitTests.Data.BogusData
{
    public class RequestLoginUserDtoBogusData
    {
        private Faker<RequestLoginUserDto> GenerateFakeRequestLoginUserDto()
        {
            return new Faker<RequestLoginUserDto>()
                .RuleFor(requestLoginUserDto => requestLoginUserDto.UserName,
                    faker => faker.Person.UserName)
                .RuleFor(requestLoginUserDto => requestLoginUserDto.Password,
                    faker => faker.Internet.Password());
        }

        public RequestLoginUserDto GenerateFakeOneValidRequestLoginUserDto()
        {
            return GenerateFakeRequestLoginUserDto().Generate();
        }
    }
}
