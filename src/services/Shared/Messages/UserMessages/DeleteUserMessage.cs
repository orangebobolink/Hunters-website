namespace Shared.Messages.UserMessages
{
    public class DeleteUserMessage
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
