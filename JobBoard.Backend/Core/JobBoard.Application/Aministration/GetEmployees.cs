using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Aministration
{
    public class GetEmployees
    {
        public class AdminEmployeesVm
        {
            public IList<Employee> Employees { get; set; }
        }

        public class GetEmployeesQuery : IRequest<AdminEmployeesVm>
        {
        }

        public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, AdminEmployeesVm>
        {
            private readonly IJobBoardDbContext _context;

            public GetEmployeesQueryHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<AdminEmployeesVm> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
            {
                return new AdminEmployeesVm { Employees = await _context.Employees.ToListAsync() };
            }
        }
    }
}