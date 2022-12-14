using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Aministration
{
    public class GetEmployees
    {
        public class EmployeesVm
        {
            public IList<Employee> Employees { get; set; }
        }

        public class GetEmployeesQuery : IRequest<EmployeesVm>
        {
        }

        public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, EmployeesVm>
        {
            private readonly IJobBoardDbContext _context;

            public GetEmployeesQueryHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<EmployeesVm> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
            {
                return new EmployeesVm { Employees = await _context.Employees.ToListAsync() };
            }
        }
    }
}