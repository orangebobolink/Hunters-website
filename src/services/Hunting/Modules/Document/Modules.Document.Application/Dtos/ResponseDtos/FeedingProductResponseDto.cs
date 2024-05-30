namespace Modules.Document.Application.Dtos.ResponseDtos
{
    public record class FeedingProductResponseDto
    {
        public Guid Id { get; init; }
        public string Product { get; init; } = string.Empty;
        public int Quantity { get; init; }
        public string UnitOfMeasurement { get; init; } = string.Empty;
    }
}
