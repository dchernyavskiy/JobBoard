using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;

namespace JobBoard.Application.Educations
{
    public class CreateEducation
    {
        public class CreateEducationCommand : IRequest<Guid>
        {
            public string Name { get; set; }
            public DateTime Start { get; set; }
            public DateTime? End { get; set; }
            public string University { get; set; }
            public string Discription { get; set; }
            public Guid EmployeeId { get; set; }
        }

        public class CreateEducationCommandHandler : IRequestHandler<CreateEducationCommand, Guid>
        {
            private readonly IJobBoardDbContext _context;

            public CreateEducationCommandHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(CreateEducationCommand request, CancellationToken cancellationToken)
            {
                var entity = new Education
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Start = request.Start,
                    End = request.End,
                    University = request.University,
                    Discription = request.Discription,
                    EmployeeId = request.EmployeeId
                };

                await _context.Educations.AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
