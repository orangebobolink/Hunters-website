namespace Modules.Document.Application.Dtos.ResponseDtos
{
    public record class UserResponseDto
    {
        public Guid Id { get; init; }
        public string Email { get; init; } = string.Empty;
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string MiddleName { get; init; } = string.Empty;
    }
}
