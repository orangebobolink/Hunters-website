namespace Rent.Application.Dtos.RequestDtos
{
    public class RentUserRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public ICollection<RentProductRequestDto> RentProducts { get; set; } = [];
    }
}
