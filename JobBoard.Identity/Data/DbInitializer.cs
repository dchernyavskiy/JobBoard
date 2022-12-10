namespace JobBoard.Identity.Data
{
    public static class DbInitializer
    {
        public static void EnsureDbCreated(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetRequiredService<AuthDbContext>();
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                }
                catch (Exception)
                {
                    // var logger = serviceProvider.GetService<ILogger>();
                    // logger.LogError(e, "An error occurred with app initialization");
                }
            }
        }
    }
}