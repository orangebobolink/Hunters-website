namespace Modules.Document.Application.Dtos.ResponseDto
{
    public class CouponResponseDto
    {
        public Guid Id { get; set; }
        public Guid PermissionId { get; set; }
        public string AnimalName { get; set; } = string.Empty;
    }
}
