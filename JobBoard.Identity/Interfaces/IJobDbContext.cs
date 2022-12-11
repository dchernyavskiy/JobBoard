using JobBoard.Domain;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Identity.Interfaces
{
    public interface IJobDbContext
    {
        DbSet<Employee> Employees { get; }
        DbSet<Employer> Employers { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}