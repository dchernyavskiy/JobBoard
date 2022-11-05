using JobBoard.Application.Common.Exceptions;
using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Educations
{
    public class DeleteEducation
    {
        public class DeleteEducationCommand : IRequest
        {
            public Guid Id { get; set; }
            public Guid EmployeeId { get; set; }
        }

        public class DeleteEducationCommandHandler : IRequestHandler<DeleteEducationCommand>
        {

            private readonly IJobBoardDbContext _context;

            public DeleteEducationCommandHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteEducationCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Educations
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.EmployeeId == request.EmployeeId);

                if (entity == null)
                    throw new NotFoundException(nameof(Education), request.Id);

                _context.Educations.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }

}
