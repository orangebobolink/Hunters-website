namespace Chat.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public List<Message> Messages { get; set; } = [];
        public List<UserGroup> Groups { get; set; } = [];
    }
}
