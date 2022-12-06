using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Persistence
{
    public class JobBoardDbContext : DbContext, IJobBoardDbContext
    {

        public JobBoardDbContext(DbContextOptions<JobBoardDbContext> opts) : base(opts) 
        {
            Database.EnsureCreated();
        }

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<Education> Educations => Set<Education>();

        public DbSet<Employee> Employees => Set<Employee>();

        public DbSet<Employer> Employers => Set<Employer>();

        public DbSet<Job> Jobs => Set<Job>();

        public DbSet<Qualification> Qualifications => Set<Qualification>();

        public DbSet<Responsibility> Responsibilities => Set<Responsibility>();

        public DbSet<Location> Locations => Set<Location>();

        public DbSet<JobEmployee> JobEmployees => Set<JobEmployee>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>()
                .HasMany(x => x.AppliedByEmployees)
                .WithMany(x => x.AppliedJobs);

            base.OnModelCreating(modelBuilder);
        }
    }
}
