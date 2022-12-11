using JobBoard.Application.Common.Exceptions;
using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Educations
{
    public class UpdateEducation
    {
        public class UpdateEducationCommand : IRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public DateTime Start { get; set; }
            public DateTime? End { get; set; }
            public string University { get; set; }
            public string Discription { get; set; }
        }

        public class UpdateEducationCommandHandler : IRequestHandler<UpdateEducationCommand>
        {
            private readonly IJobBoardDbContext _context;

            public UpdateEducationCommandHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Educations
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (entity == null)
                    throw new NotFoundException(nameof(Education), request.Id);

                entity.Name = request.Name;
                entity.Start = request.Start;
                entity.End = request.End;
                entity.University = request.University;
                entity.Discription = request.Discription;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}