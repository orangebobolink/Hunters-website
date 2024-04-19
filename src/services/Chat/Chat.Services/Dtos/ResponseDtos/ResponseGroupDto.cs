namespace Chat.Services.Dtos.ResponseDtos
{
    public class ResponseGroupDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<ResponseUserDto> Users { get; set; } = new();
        public List<ResponseMessageDto> Messages { get; set; } = new();
    }
}
