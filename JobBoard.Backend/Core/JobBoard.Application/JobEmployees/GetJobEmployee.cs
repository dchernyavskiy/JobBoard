using AutoMapper;
using JobBoard.Application.Common.Exceptions;
using JobBoard.Application.Common.Mappings;
using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.JobEmployees
{
    public class GetJobEmployee
    {
        public class JobEmployeeVm : IMapWith<JobEmployee>
        {
            public Guid Id { get; set; }
            public Guid? EmployeeId { get; set; }
            public Guid JobId { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<JobEmployee, JobEmployeeVm>();
            }
        }

        public class GetJobEmployeeQuery : IRequest<JobEmployeeVm>
        {
            public Guid Id { get; set; }
        }

        public class GetJobEmployeeQueryHandler : IRequestHandler<GetJobEmployeeQuery, JobEmployeeVm>
        {
            private readonly IJobBoardDbContext _context;
            private readonly IMapper _mapper;

            public GetJobEmployeeQueryHandler(IJobBoardDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<JobEmployeeVm> Handle(GetJobEmployeeQuery request, CancellationToken cancellationToken)
            {
                var entity = await _context.JobEmployees
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (entity == null)
                    throw new NotFoundException(nameof(JobEmployee), request.Id);

                return _mapper.Map<JobEmployeeVm>(entity);
            }
        }
    }
}