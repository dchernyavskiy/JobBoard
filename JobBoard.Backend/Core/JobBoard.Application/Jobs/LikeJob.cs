using JobBoard.Application.Common.Exceptions;
using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Jobs
{
    public class LikeJob
    {
        public class LikeJobCommand : IRequest<Guid>
        {
            public Guid EmployeeId { get; set; }
            public Guid JobId { get; set; }
        }

        public class LikeJobCommandHandler : IRequestHandler<LikeJobCommand, Guid>
        {
            private readonly IJobBoardDbContext _context;

            public LikeJobCommandHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(LikeJobCommand request, CancellationToken cancellationToken)
            {
                var job = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == request.JobId);

                if (job == null) throw new NotFoundException(nameof(Job), request.JobId);

                var likeJob = new EmployeeLikeJob
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = request.EmployeeId,
                    JobId = request.JobId
                };

                await _context.EmployeeLikeJobs.AddAsync(likeJob);
                await _context.SaveChangesAsync(cancellationToken);

                return likeJob.Id;
            }
        }
    }
}