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
    public class GetEmployers
    {
        public class EmployersVm
        {
            public IList<Employer> Employers { get; set; }
        }

        public class GetEmployersQuery : IRequest<EmployersVm>
        {
        }

        public class GetEmployersQueryHandler : IRequestHandler<GetEmployersQuery, EmployersVm>
        {
            private readonly IJobBoardDbContext _context;

            public GetEmployersQueryHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<EmployersVm> Handle(GetEmployersQuery request, CancellationToken cancellationToken)
            {
                return new EmployersVm { Employers = await _context.Employers.ToListAsync() };
            }
        }
    }
}