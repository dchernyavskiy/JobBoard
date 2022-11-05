using JobBoard.Application.Common.Exceptions;
using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Employees
{
    public class UpdateEmployee
    {
        public class UpdateEmployeeCommand : IRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Discription { get; set; }
            public DateTime DatePosted { get; set; }
            public string Location { get; set; }
            public int Hours { get; set; }
            public int SalaryStart { get; set; }
            public int SalaryEnd { get; set; }
            public int Experience { get; set; }
            public Guid EmployerId { get; set; }
        }


        public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
        {

            private readonly IJobBoardDbContext _context;

            public UpdateEmployeeCommandHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Employees
                    .FirstOrDefaultAsync(x => x.Id == request.Id , cancellationToken);

                if (entity == null)
                    throw new NotFoundException(nameof(Employee), request.Id);

                //entity.Name = request.Name;
                //entity.Discription = request.Discription;
                //entity.DatePosted = request.DatePosted;
                //entity.Location = request.Location;
                //entity.Hours = request.Hours;
                //entity.SalaryStart = request.SalaryStart;
                //entity.SalaryEnd = request.SalaryEnd;
                //entity.Experience = request.Experience;


                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
