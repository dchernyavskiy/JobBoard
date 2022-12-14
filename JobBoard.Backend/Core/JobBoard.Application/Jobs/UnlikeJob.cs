using JobBoard.Application.Common.Exceptions;
using JobBoard.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Jobs
{
    public class UnlikeJob
    {
        public class UnlikeJobCommand : IRequest
        {
            public Guid EmployeeId { get; set; }
            public Guid JobId { get; set; }
        }

        public class UnlikeJobHandler : IRequestHandler<UnlikeJobCommand>
        {
            private readonly IJobBoardDbContext _context;

            public UnlikeJobHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UnlikeJobCommand request, CancellationToken cancellationToken)
            {
                var likedJob = await _context.EmployeeLikeJobs
                    .FirstOrDefaultAsync(x => x.EmployeeId == request.EmployeeId
                    && x.JobId == request.JobId);

                if (likedJob == null)
                    throw new Exception("Liked Job not found");

                _context.EmployeeLikeJobs.Remove(likedJob);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}