using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using System.Diagnostics.Metrics;

namespace JobBoard.Application.Employees
{
    public class CreateEmployee
    {
        public class CreateEmployeeCommand : IRequest<Guid>
        {
            public string? Website { get; set; }
            public string Country { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            public string? Zip { get; set; }
        }

        public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Guid>
        {
            private readonly IJobBoardDbContext _context;

            public CreateEmployeeCommandHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
            {
                var entity = new Employee
                {
                    Id = Guid.NewGuid(),
                    //Website = request.Website,
                    //Country = request.Country,
                    //State = request.State,
                    //City = request.City,
                    //Zip = request.Zip
                };

                await _context.Employees.AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
