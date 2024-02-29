namespace Identity.Services.Dtos.ResponseDtos
{
    public class ResponseAuthenticatedDto
    {
        public string? Token { get; set; }
        public ResponseUserDto? User { get; set; }
    }
}
