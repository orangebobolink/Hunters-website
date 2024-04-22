namespace Modules.Document.Application.Dtos.ResponseDtos
{
    public record class AnimalResponseDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Type { get; init; } = string.Empty;
    }
}
