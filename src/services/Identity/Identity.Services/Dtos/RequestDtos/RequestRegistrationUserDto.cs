using Identity.Domain.Enums;

namespace Identity.Services.Dtos.RequestDtos
{
    public record class RequestRegistrationUserDto
    {
        public string Email { get; init; } = string.Empty;
        public string PhoneNumber { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        public Sex Sex { get; set; }
    }
}
