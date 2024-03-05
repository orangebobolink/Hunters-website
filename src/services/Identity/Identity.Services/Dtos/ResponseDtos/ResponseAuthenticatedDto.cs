namespace Identity.Services.Dtos.ResponseDtos
{
    public class ResponseAuthenticatedDto
    {
        public string? AccessToken { get; set; }
        public ResponseUserDto? User { get; set; }
    }
}
