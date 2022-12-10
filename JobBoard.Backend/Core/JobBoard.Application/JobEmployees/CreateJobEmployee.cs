using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;

namespace JobBoard.Application.JobEmployees
{
    public class CreateJobEmployee
    {
        public class CreateJobEmployeeCommand : IRequest<Guid>
        {
            public Guid EmployeeId { get; set; }
            public Guid JobId { get; set; }
        }

        public class CreateJobEmployeeCommandHandler : IRequestHandler<CreateJobEmployeeCommand, Guid>
        {
            private readonly IJobBoardDbContext _context;

            public CreateJobEmployeeCommandHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(CreateJobEmployeeCommand request, CancellationToken cancellationToken)
            {
                var entity = new JobEmployee
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = request.EmployeeId,
                    JobId = request.JobId
                };

                await _context.JobEmployees.AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}