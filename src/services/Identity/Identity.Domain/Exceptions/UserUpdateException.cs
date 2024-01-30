namespace Identity.Domain.Exceptions
{
    public class UserUpdateException : InvalidOperationException
    {
        public UserUpdateException()
            : base("Failed to update user.")
        {
        }
    }
}
