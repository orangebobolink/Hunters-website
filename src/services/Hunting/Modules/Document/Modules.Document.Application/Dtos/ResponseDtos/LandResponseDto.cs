namespace Modules.Document.Application.Dtos.ResponseDtos
{
    public record class LandResponseDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}
