using Identity.Domain.Enums;

namespace Identity.Services.Dtos.ResponseDtos
{
    public class ResponseUserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        public string AvatarUrl { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public List<string> RoleNames { get; set; } = [];
    }
}
