using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                catch (Exception e)
                {
                    var logger = serviceProvider.GetService<ILogger>();
                    logger.LogError(e, "An error occurred with app initialization");
                }
            }
        }
    }
}
