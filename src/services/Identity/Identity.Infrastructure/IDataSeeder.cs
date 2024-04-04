namespace Identity.Infrastructure
{
    public interface IDataSeeder
    {
        public Task SeedAsync();
        public Task SeedMessageAsync();
    }
}
