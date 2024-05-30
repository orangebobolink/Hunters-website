using Identity.Services.Dtos.RequestDtos;
using Bogus;

namespace Identity.UnitTests.Helpers.BogusFaker
{
    public class RequestLoginUserDtoFakerHelper
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
