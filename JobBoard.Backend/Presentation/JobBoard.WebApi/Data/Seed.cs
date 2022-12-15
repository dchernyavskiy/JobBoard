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

            var erIds = new List<string>
            {
                "041343ea-0f3d-458b-9fb6-7bd6700d69e8",
                "010c4d66-7268-44a9-a991-e6d5aadea719",
                "ad4d1381-49a1-460e-aee8-808a5b8ed2da",
                "cc653465-e4a0-40fa-99dc-061841fbf76f",
            };
            var count = 0;
            var employers2 = new Faker<Employer>()
                .Rules((f, e) =>
                {
                    e.Id = Guid.Parse(erIds[count++]);
                    e.Name = f.Company.CompanyName();
                    e.AboutUs = f.Random.Words(100);
                    e.Location = f.Address.City();
                    e.PhotoLink = f.Internet.Url();
                    e.TeamSize = f.Random.Int(100, 1000);
                })
                .Generate(4);

            context.Employers.AddRange(employers);
            context.Employers.AddRange(employers2);
            context.SaveChanges();

            var jobs = new Faker<Job>()
                .Rules((f, j) =>
                {
                    j.Id = Guid.NewGuid();
                    j.Name = f.Commerce.Product();
                    j.Discription = f.Random.Words(100);
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

            var employer = new Faker<Employer>()
                .Rules((f, e) =>
                {
                    e.Id = Guid.Parse("0c207243-5fb9-4a2d-9581-cab3e01b2609");
                    e.Name = f.Company.CompanyName();
                    e.AboutUs = f.Random.Words(100);
                    e.Location = f.Address.City();
                    e.PhotoLink = f.Internet.Url();
                    e.TeamSize = f.Random.Int(100, 1000);
                })
                .Generate(1);

            context.Employers.AddRange(employer);

            context.SaveChanges();

            var employee = new Faker<Employee>()
                .Rules((f, e) =>
                {
                    e.Id = Guid.Parse("041343ea-0f3d-458b-9fb6-7bd6700d69e8");
                    e.Email = "tom.smith@mail.com";
                    e.FirstName = "Tom";
                    e.LastName = "Smith";
                    e.CVLink = f.Internet.Url();
                    e.Phone = "+329813923";
                    e.IsBan = false;
                })
                .Generate(1);
            context.Employees.AddRange(employee);
            context.SaveChanges();
        }
    }
}