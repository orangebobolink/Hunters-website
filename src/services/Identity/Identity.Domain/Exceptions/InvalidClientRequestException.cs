namespace Identity.Domain.Exceptions
{
    public class InvalidClientRequestException : BadRequestException
    {
        public InvalidClientRequestException()
            : base("Invalid client request exception")
        {
        }
    }
}
