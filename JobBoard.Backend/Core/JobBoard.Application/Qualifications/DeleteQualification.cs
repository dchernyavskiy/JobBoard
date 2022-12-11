using JobBoard.Application.Common.Exceptions;
using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Qualifications
{
    public class DeleteQualification
    {
        public class DeleteQualificationCommand : IRequest
        {
            public Guid Id { get; set; }
            public Guid EmployerId { get; set; }
        }

        public class DeleteQualificationCommandHandler : IRequestHandler<DeleteQualificationCommand>
        {
            private readonly IJobBoardDbContext _context;

            public DeleteQualificationCommandHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteQualificationCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Qualifications
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (entity == null)
                    throw new NotFoundException(nameof(Qualification), request.Id);

                _context.Qualifications.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}