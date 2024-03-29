namespace Shared.Messages.UserMessages
{
    public record class DeleteUserMessage
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
