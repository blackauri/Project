namespace Project.Persistence
{
    public static class Seeder
    {
        public static Task Seed(this ProjectIdentityDbContext db)
        {
            db.Database.EnsureCreated();

            // Add Seeding

            return Task.CompletedTask;
        }
    }
}
