namespace Chat.Domain.Entities
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<User> Members { get; set; } = new();
        public List<Message> Messages { get; set; } = new();
        public bool IsDeleted { get; set; } = false;
        public DateTime DeleteTime { get; set; }
    }
}
