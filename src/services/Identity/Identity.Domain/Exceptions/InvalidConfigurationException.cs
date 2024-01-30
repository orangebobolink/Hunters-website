namespace Identity.Domain.Exceptions
{
    public class InvalidConfigurationException : InvalidOperationException
    {
        public InvalidConfigurationException()
           : base("Invalid data in configuration")
        {
        }
    }
}
