namespace Identity.Domain.Helpers
{
    public static class UserErrorHelper
    {
        public const string EmptyUsernameError = "Username is required.";
        public const string InvalidUsernameError = "Username must be between 4 and 20 characters.";
        public const string EmptyEmailError = "Email is required.";
        public const string InvalidEmailError = "Email is invalid.";
        public const string EmptyPasswordError = "Password is required.";
        public const string InvalidPasswordError = "Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character.";
        public const string InvalidMinimumLengthPasswordError = "Password must be at least 8 characters long.";
        public const string EmptyPhoneError = "Phone number is required.";
        public const string InvalidPhoneError = "Invalid phone number format.";
    }
}
