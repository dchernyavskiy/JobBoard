using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Jobs
{
    public class CreateJob
    {
        public class CreateJobCommand : IRequest<Guid>
        {
            public string Name { get; set; }
            public string Discription { get; set; }
            public DateTime DatePosted { get; set; }
            public string Location { get; set; }
            public int Hours { get; set; }
            public int SalaryStart { get; set; }
            public int SalaryEnd { get; set; }
            public int Experience { get; set; }
            public Guid EmployerId { get; set; }
            public string Employment { get; set; }
            public Guid CategoryId { get; set; }
        }

        public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Guid>
        {
            private readonly IJobBoardDbContext _context;

            public CreateJobCommandHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(CreateJobCommand request, CancellationToken cancellationToken)
            {
                var entity = new Job
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Discription = request.Discription,
                    DatePosted = request.DatePosted,
                    Hours = request.Hours,
                    SalaryStart = request.SalaryStart,
                    SalaryEnd = request.SalaryEnd,
                    Experience = request.Experience,
                    EmployerId = request.EmployerId,
                    Employment = request.Employment,
                    CategoryId = request.CategoryId
                };

                var location = _context.Locations.FirstOrDefault(x => x.City == request.Location);
                if (location == null)
                {
                    location = new Location
                    {
                        Id = Guid.NewGuid(),
                        City = request.Location
                    };
                    await _context.Locations.AddAsync(location);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                entity.LocationId = location.Id;
                await _context.Jobs.AddAsync(entity);

                var e = await _context.Employers.FirstOrDefaultAsync(x => x.Id == request.EmployerId);
                var es = await _context.Employers.ToListAsync();
                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}