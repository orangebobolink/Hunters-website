namespace Modules.Document.Application.Dtos.ResponseDto
{
    public class FeedingProductResponseDto
    {
        public Guid Id { get; set; }
        public string Product { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string UnitOfMeasurement { get; set; } = string.Empty;
    }
}
