using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Persistence
{
    public class JobBoardDbContext : DbContext, IJobBoardDbContext
    {

        public JobBoardDbContext(DbContextOptions<JobBoardDbContext> opts) : base(opts) { }

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<Education> Educations => Set<Education>();

        public DbSet<Employee> Employees => Set<Employee>();

        public DbSet<Employer> Employers => Set<Employer>();

        public DbSet<Job> Jobs => Set<Job>();

        public DbSet<Qualification> Qualifications => Set<Qualification>();

        public DbSet<Responsibility> Responsibilities => Set<Responsibility>();

        public DbSet<Location> Locations => Set<Location>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {              
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Employer>()
            //    .HasData(new Employer
            //    {
            //        Id = Guid.Parse("041343ea-0f3d-458b-9fb6-7bd6700d69e8"),
            //        Name = "Employeer",
            //        AboutUs = "Super emloyeer",
            //        Responsibilities = "Some responsilities"
            //    });
        }
    }
}
