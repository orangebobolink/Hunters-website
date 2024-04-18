namespace Modules.Document.Domain.Entities
{
    public class Feeding
    {
        public Guid Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public DateTime FeedingDate { get; set; }
        public Guid IssuedId { get; set; }
        public User? Issued { get; set; }
        public Guid ReceivedId { get; set; }
        public User? Received { get; set; }
        public DateTime ReceivedDate = DateTime.Now;
        public List<FeedingProduct> Products { get; set; } = [];
    }
}
