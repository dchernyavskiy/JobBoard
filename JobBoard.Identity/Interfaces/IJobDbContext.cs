using JobBoard.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Identity.Interfaces
{
    public interface IJobDbContext
    {
        DbSet<Employee> Employees { get; }
        DbSet<Employer> Employers { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
