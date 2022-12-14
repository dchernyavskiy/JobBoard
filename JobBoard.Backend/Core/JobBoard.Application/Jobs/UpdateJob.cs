using JobBoard.Application.Common.Exceptions;
using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Jobs
{
    public class UpdateEducation
    {
        public class UpdateJobCommand : IRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Discription { get; set; }
            public DateTime DatePosted { get; set; }
            public string Location { get; set; }
            public int Hours { get; set; }
            public int SalaryStart { get; set; }
            public int SalaryEnd { get; set; }
            public int Experience { get; set; }
            public Guid EmployerId { get; set; }
        }

        public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand>
        {
            private readonly IJobBoardDbContext _context;

            public UpdateJobCommandHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Jobs
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.EmployerId == request.EmployerId, cancellationToken);

                if (entity == null)
                    throw new NotFoundException(nameof(Job), request.Id);

                var location = _context.Locations.FirstOrDefault(x => x.City == request.Location);
                if (location == null)
                {
                    location = new Location
                    {
                        Id = Guid.NewGuid(),
                        City = request.Location
                    };
                    await _context.Locations.AddAsync(location);
                }

                entity.Name = request.Name;
                entity.Discription = request.Discription;
                entity.DatePosted = request.DatePosted;
                entity.LocationId = location.Id;
                entity.Hours = request.Hours;
                entity.SalaryStart = request.SalaryStart;
                entity.SalaryEnd = request.SalaryEnd;
                entity.Experience = request.Experience;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}