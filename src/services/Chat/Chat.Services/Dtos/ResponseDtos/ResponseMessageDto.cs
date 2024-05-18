namespace Chat.Services.Dtos.ResponseDtos
{
    public class ResponseMessageDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ToUserId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
