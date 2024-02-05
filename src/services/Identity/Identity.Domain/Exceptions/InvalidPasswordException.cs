namespace Identity.Domain.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException()
            : base("Invalid password provided.")
        {
        }
    }
}
