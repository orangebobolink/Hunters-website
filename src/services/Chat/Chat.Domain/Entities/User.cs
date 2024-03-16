namespace Chat.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public List<Message> Messages { get; set; } = new();
    }
}
