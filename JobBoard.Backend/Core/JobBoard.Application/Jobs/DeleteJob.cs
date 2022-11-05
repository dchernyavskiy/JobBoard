using JobBoard.Application.Common.Exceptions;
using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Stores
{
    public class DeleteJob
    {
        public class DeleteJobCommand : IRequest
        {
            public Guid Id { get; set; }
            public Guid EmployerId { get; set; }
        }

        public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand>
        {

            private readonly IJobBoardDbContext _context;

            public DeleteJobCommandHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Jobs
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.EmployerId == request.EmployerId);

                if (entity == null)
                    throw new NotFoundException(nameof(Job), request.Id);

                _context.Jobs.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }

}
