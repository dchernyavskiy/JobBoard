
using JobBoard.Application.Common.Exceptions;
using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Locations
{
    public class DeleteLocation
    {
        public class DeleteLocationCommand : IRequest
        {
            public Guid Id { get; set; }
        }

        public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand>
        {

            private readonly IJobBoardDbContext _context;

            public DeleteLocationCommandHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Locations
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (entity == null)
                    throw new NotFoundException(nameof(Location), request.Id);

                _context.Locations.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
