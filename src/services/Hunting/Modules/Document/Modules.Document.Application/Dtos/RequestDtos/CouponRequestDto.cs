namespace Modules.Document.Application.Dtos.RequestDtos
{
    public class CouponRequestDto
    {
        public Guid PermissionId { get; set; }
        public string AnimalName { get; set; } = string.Empty;
    }
}
