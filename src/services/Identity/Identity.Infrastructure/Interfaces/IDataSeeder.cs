namespace Identity.Infrastructure.Interfaces
{
    public interface IDataSeeder
    {
        public Task SeedAsync();
        public Task SeedMessageAsync();
    }
}
