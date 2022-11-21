using JobBoard.Domain;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Interfaces
{
    public interface IJobBoardDbContext
    {
        DbSet<Category> Categories { get; }
        DbSet<Education> Educations { get; }
        DbSet<Employee> Employees { get; }
        DbSet<Employer> Employers { get; }
        DbSet<Job> Jobs { get; }
        DbSet<Qualification> Qualifications { get; }
        DbSet<Responsibility> Responsibilities { get; }
        DbSet<Location> Locations { get; }
        DbSet<JobEmployee> JobEmployees { get; }

        Task<int> SaveChangesAsync(CancellationToken token);
    }
}
