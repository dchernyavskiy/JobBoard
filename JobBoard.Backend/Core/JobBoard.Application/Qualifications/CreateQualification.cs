using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;

namespace JobBoard.Application.Qualifications
{
    public class CreateQualification
    {
        public class CreateQualificationCommand : IRequest<Guid>
        {
            public string Discription { get; set; }
            public Guid JobId { get; set; }
        }

        public class CreateQualificationCommandHandler : IRequestHandler<CreateQualificationCommand, Guid>
        {
            private readonly IJobBoardDbContext _context;

            public CreateQualificationCommandHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(CreateQualificationCommand request, CancellationToken cancellationToken)
            {
                var entity = new Qualification
                {
                    Id = Guid.NewGuid(),
                    Discription = request.Discription,
                    JobId = request.JobId
                };

                await _context.Qualifications.AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}