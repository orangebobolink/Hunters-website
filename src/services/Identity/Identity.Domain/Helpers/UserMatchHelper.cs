namespace Identity.Domain.Helpers
{
    public static class UserMatchHelper
    {
        public const string PasswordMatch = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W)";
        public const string PhoneMatch = @"^\+\d{1,3}-\d{1,14}$";
    }
}
