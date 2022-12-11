using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;

namespace JobBoard.Application.Locations
{
    public class CreateLocation
    {
        public class CreateLocationCommand : IRequest<Guid>
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, Guid>
        {
            private readonly IJobBoardDbContext _context;

            public CreateLocationCommandHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
            {
                var entity = new Location
                {
                    Id = Guid.NewGuid(),
                    City = request.Name
                };

                await _context.Locations.AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}