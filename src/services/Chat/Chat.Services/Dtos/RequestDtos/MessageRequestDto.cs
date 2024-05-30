namespace Chat.Services.Dtos.RequestDtos
{
    public class MessageRequestDto
    {
        public Guid GroupId { get; set; }
        public Guid UserId { get; set; }
        public Guid ToUserId { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}