namespace Modules.Document.Domain.Entities
{
    public class PermissionForExtractionOfHuntingAnimal
    {
        public Guid Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Number { get; set; } = string.Empty;
        public Guid AnimalId { get; set; }

    }
}
