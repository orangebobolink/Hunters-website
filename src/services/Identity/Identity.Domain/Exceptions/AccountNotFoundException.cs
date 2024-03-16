namespace Identity.Domain.Exceptions
{
    public sealed class AccountNotFoundException : NotFoundException
    {
        public AccountNotFoundException(string username)
             : base($"The account with Username {username} was not found.")
        {
        }

        public AccountNotFoundException(Guid accountId)
            : base($"The account with Id {accountId} was not found.")
        {
        }
    }
}
