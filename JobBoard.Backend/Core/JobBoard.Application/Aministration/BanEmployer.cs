using JobBoard.Application.Common.Exceptions;
using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Application.Aministration
{
    public class BanEmployer
    {
        public class BanEmployerCommand : IRequest
        {
            public Guid UserId { get; set; }
        }

        public class BanEmployerCommandHandler : IRequestHandler<BanEmployerCommand>
        {
            private readonly IJobBoardDbContext _context;

            public BanEmployerCommandHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(BanEmployerCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.Employers
                    .FirstOrDefaultAsync(x => x.Id == request.UserId);

                if (user == null) throw new NotFoundException(nameof(Employer), request.UserId);

                user.IsBan = !user.IsBan;
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}