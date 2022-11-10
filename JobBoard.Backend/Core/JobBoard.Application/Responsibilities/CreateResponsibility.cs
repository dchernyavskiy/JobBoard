using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using static JobBoard.Application.Responsibilities.CreateResponsibility;

namespace JobBoard.Application.Responsibilities
{
    public class CreateResponsibility
    {
        public class CreateResponsibilityCommand : IRequest<Guid>
        {
            public string Discription { get; set; }
            public Guid JobId { get; set; }
        }
    }

    public class CreateResponsibilityCommandHandler : IRequestHandler<CreateResponsibilityCommand, Guid>
    {
        private readonly IJobBoardDbContext _context;

        public CreateResponsibilityCommandHandler(IJobBoardDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateResponsibilityCommand request, CancellationToken cancellationToken)
        {
            var entity = new Responsibility
            {
                Id = Guid.NewGuid(),
                Discription = request.Discription,
                JobId = request.JobId
            };
            await _context.Responsibilities.AddAsync(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;        }
    }
}