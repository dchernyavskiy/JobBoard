using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JobBoard.Application.Stores
{
    public class CreateJob
    {
        public class CreateJobCommand : IRequest<Guid>
        {
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

        public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Guid>
        {
            private readonly IJobBoardDbContext _context;

            public CreateJobCommandHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(CreateJobCommand request, CancellationToken cancellationToken)
            {
                var entity = new Job
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Discription = request.Discription ,
                    DatePosted = request.DatePosted ,
                    Location = request.Location ,
                    Hours = request.Hours ,
                    SalaryStart = request.SalaryStart ,
                    SalaryEnd = request.SalaryEnd ,
                    Experience = request.Experience ,
                    EmployerId = request.EmployerId,
                };
                
                await _context.Jobs.AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
