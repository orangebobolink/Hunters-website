namespace Rent.Application.Dtos.ResponseDtos
{
    public record class RentUserResponseDto
    {
        public Guid Id { get; set; }
        public string Email { get; init; } = string.Empty;
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string MiddleName { get; init; } = string.Empty;
        public ICollection<RentProductResponseDto> RentProducts { get; init; } = [];
    }
}
