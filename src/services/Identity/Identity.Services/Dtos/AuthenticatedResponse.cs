using Identity.Services.Dtos.ResponseDtos;

namespace Identity.Services.Dtos
{
    public class AuthenticatedResponse
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public ResponseUserDto? User { get; set; }
    }
}
