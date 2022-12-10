using JobBoard.Application.Common.Exceptions;
using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Employers
{
    public class UpdateEmployer
    {
        public class UpdateEmployerCommand : IRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string AboutUs { get; set; }
            public int TeamSize { get; set; }
            public string Location { get; set; }
            public string PhotoLink { get; set; }
        }

        public class UpdateEmployerCommandHandler : IRequestHandler<UpdateEmployerCommand>
        {
            private readonly IJobBoardDbContext _context;

            public UpdateEmployerCommandHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateEmployerCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Employers
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (entity == null)
                    throw new NotFoundException(nameof(Job), request.Id);

                entity.Name = request.Name;
                entity.AboutUs = request.AboutUs;
                entity.TeamSize = request.TeamSize;
                entity.Location = request.Location;
                entity.PhotoLink = request.PhotoLink;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}