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
    public class BanEmployee
    {
        public class BanEmployeeCommand : IRequest
        {
            public Guid UserId { get; set; }
        }

        public class BanEmployeeCommandHandler : IRequestHandler<BanEmployeeCommand>
        {
            private readonly IJobBoardDbContext _context;

            public BanEmployeeCommandHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(BanEmployeeCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.Employees
                    .FirstOrDefaultAsync(x => x.Id == request.UserId);

                if (user == null) throw new NotFoundException(nameof(Employee), request.UserId);

                user.IsBan = !user.IsBan;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}