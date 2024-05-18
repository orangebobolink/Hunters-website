namespace Modules.Animal.Application.Dtos.ResponseDtos
{
    public class HuntingSeasonResponseDto
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        public string WayOfHunting { get; set; } = string.Empty;
        public string Weapon { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
    }
}
