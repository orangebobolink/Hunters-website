namespace Modules.Animal.Application.Dtos.ResponseDtos
{
    public class AnimalInfoResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public List<HuntingSeasonResponseDto> HuntingSeasons { get; set; } = [];
    }
}
