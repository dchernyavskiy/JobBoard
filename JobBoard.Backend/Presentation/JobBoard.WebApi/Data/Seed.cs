using Bogus;
using JobBoard.Domain;
using JobBoard.Persistence;

namespace JobBoard.WebApi.Data
{
    public class Seed
    {
        public static void Initialize(JobBoardDbContext context)
        {
            if (context.Jobs.Any()) return;

            var categories = new Faker<Category>()
                .Rules((f, c) =>
                {
                    c.Id = Guid.NewGuid();
                    c.Name = f.Commerce.Categories(1)[0];
                })
                .Generate(10);
            context.Categories.AddRange(categories);


            var locations = new Faker<Location>()
                .Rules((f, l) =>
                {
                    l.Id = Guid.NewGuid();
                    l.City = f.Address.City();
                })
                .Generate(10);

            context.Locations.AddRange(locations);
            context.SaveChanges();

            var employers = new Faker<Employer>()
                .Rules((f, e) =>
                {
                    e.Id = Guid.NewGuid();
                    e.Name = f.Company.CompanyName();
                    e.AboutUs = f.Random.Words(100);
                    e.Location = f.Address.City();
                    e.PhotoLink = f.Internet.Url();
                    e.TeamSize = f.Random.Int(100, 1000);
                })
                .Generate(10);

            context.Employers.AddRange(employers);
            context.SaveChanges();

            var jobs = new Faker<Job>()
                .Rules((f, j) =>
                {
                    j.Id = Guid.NewGuid();
                    j.Name = f.Commerce.Product();
                    j.ShortDiscription = f.Commerce.ProductDescription();
                    j.Discription = f.Commerce.ProductDescription();
                    j.DatePosted = f.Date.Past(1);
                    j.LocationId = f.Random.CollectionItem(locations.Select(x => x.Id).ToList());
                    j.Hours = f.Random.Int(8, 16);
                    j.SalaryStart = f.Random.Int(200, 15000);
                    j.SalaryEnd = f.Random.Int(500, 30000);
                    j.Experience = f.Random.Int(1, 5);
                    j.Employment = "Full time";
                    j.EmployerId = f.Random.CollectionItem(employers.Select(x => x.Id).ToList());
                    j.CategoryId = f.Random.CollectionItem(categories.Select(x => x.Id).ToList());
                })
                .Generate(100);

            var aa = jobs.Where(x => !employers.Select(x => x.Id).Contains(x.EmployerId)).ToList();

            context.Jobs.AddRange(jobs);
            context.SaveChanges();
        }
    }
}
