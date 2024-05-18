namespace Identity.Services.Dtos.RequestDtos
{
    public record class RequestUserDto
    {
        public string Email { get; init; } = string.Empty;
        public string PhoneNumber { get; init; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public List<string> RoleNames { get; set; } = [];
    }
}
