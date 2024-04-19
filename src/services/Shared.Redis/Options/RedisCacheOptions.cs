namespace Shared.Redis.Options
{
    public class RedisCacheOptions
    {
        public const string Section = "redis";
        public int SlidingExpirationTimeInMinutes { get; set; }
    }
}
