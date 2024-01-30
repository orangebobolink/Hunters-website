namespace Identity.Domain.Exceptions
{
    public class InvalidTokenException : BadRequestException
    {
        public InvalidTokenException()
            : base("Invalid refresh token")
        {
        }
    }
}
