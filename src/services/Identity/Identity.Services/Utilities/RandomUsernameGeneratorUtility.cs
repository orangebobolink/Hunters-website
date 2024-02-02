namespace Identity.Services.Utilities
{
    internal static class RandomUsernameGeneratorUtility
    {
        public static string GenerateRandomUsername()
        {
            char[] availableChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

            Random random = new Random();
            int usernameLength = 8;

            char[] username = new char[usernameLength];

            for(int i = 0; i < usernameLength; i++)
            {
                username[i] = availableChars[random.Next(availableChars.Length)];
            }

            return new string(username);
        }
    }
}
