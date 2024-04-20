namespace Modules.Document.Application.Dtos.ResponseDtos
{
    public class AnimalResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
