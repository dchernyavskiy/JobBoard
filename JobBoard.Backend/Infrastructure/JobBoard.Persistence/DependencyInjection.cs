using JobBoard.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobBoard.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IJobBoardDbContext, JobBoardDbContext>(opts =>
            {
                opts.UseSqlServer(configuration["DbConnection"], b =>
                {
                    b.MigrationsAssembly("JobBoard.WebApi");
                });
            });
            return services;
        }
    }
}
