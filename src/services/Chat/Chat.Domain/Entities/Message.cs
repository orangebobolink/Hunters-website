namespace Chat.Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public Group? Group { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime DeleteTime { get; set; }
    }
}
