namespace Identity.Services.Dtos.RequestDtos
{
    public record class UserChangePasswordRequestDto(
        Guid Id,
        string Password);
}
