using JobBoard.Domain;
using JobBoard.Identity.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Identity.Data
{
    public class JobDbContext : DbContext, IJobDbContext
    {
        public DbSet<Employee> Employees => Set<Employee>();

        public DbSet<Employer> Employers => Set<Employer>();

        public JobDbContext(DbContextOptions<JobDbContext> opts) : base(opts)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Employer>()
                .HasKey(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}