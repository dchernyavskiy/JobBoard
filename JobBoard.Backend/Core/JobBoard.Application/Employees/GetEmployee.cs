using AutoMapper;
using JobBoard.Application.Common.Exceptions;
using JobBoard.Application.Common.Mappings;
using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Employees
{
    public class GetEmployee
    {
        public class EmployeeVm : IMapWith<Employee>
        {
            public Guid Id { get; set; }
            public string? Website { get; set; }
            public string Country { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            public string? Zip { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<Employee, EmployeeVm>();
            }
        }

        public class GetEmployeeQuery : IRequest<EmployeeVm>
        {
            public Guid Id { get; set; }
        }

        public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, EmployeeVm>
        {

            private readonly IJobBoardDbContext _context;
            private readonly IMapper _mapper;

            public GetEmployeeQueryHandler(IJobBoardDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<EmployeeVm> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
            {
                var entity = await _context.Employees
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (entity == null)
                    throw new NotFoundException(nameof(Employee), request.Id);

                return _mapper.Map<EmployeeVm>(entity);
            }
        }
    }
}
