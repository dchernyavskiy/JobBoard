using JobBoard.Application.Common.Exceptions;
using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Responsibilities
{
    public class DeleteResponsibility
    {
        public class DeleteResponsibilityCommand : IRequest
        {
            public Guid Id { get; set; }
        }

        public class DeleteResponsibilityCommandHandler : IRequestHandler<DeleteResponsibilityCommand>
        {
            private readonly IJobBoardDbContext _context;

            public DeleteResponsibilityCommandHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteResponsibilityCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Responsibilities
                   .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (entity == null)
                    throw new NotFoundException(nameof(Responsibility), request.Id);

                _context.Responsibilities.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
