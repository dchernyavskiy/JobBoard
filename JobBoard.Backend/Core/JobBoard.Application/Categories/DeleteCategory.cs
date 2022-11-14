using JobBoard.Application.Common.Exceptions;
using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Categories
{
    public class DeleteCategory
    {
        public class DeleteCategoryCommand : IRequest
        {
            public Guid Id { get; set; }
        }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
        {

            private readonly IJobBoardDbContext _context;

            public DeleteCategoryCommandHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Categories
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (entity == null)
                    throw new NotFoundException(nameof(Category), request.Id);

                _context.Categories.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }

}
