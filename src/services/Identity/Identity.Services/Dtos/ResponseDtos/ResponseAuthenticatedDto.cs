namespace Identity.Services.Dtos.ResponseDtos
{
    public class ResponseAuthenticatedDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new List<string>();
        public string? AccessToken { get; set; }
    }
}
