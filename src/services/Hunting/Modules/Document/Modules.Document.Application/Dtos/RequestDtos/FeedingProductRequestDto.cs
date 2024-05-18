namespace Modules.Document.Application.Dtos.RequestDtos
{
    public class FeedingProductRequestDto
    {
        public string Product { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string UnitOfMeasurement { get; set; } = string.Empty;
    }
}
