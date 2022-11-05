using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;

namespace JobBoard.Application.Categories
{
    public class CreateCategory
    {
        public class CreateCategoryCommand : IRequest<Guid>
        {
            public string Name { get; set; }
        }

        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
        {
            private readonly IJobBoardDbContext _context;

            public CreateCategoryCommandHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                var entity = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                };

                await _context.Categories.AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
