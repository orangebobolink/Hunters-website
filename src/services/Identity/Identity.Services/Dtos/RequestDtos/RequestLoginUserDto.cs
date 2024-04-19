namespace Identity.Services.Dtos.RequestDtos
{
    public record class RequestLoginUserDto
    {
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }
}
