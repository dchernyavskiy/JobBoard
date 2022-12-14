using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobBoard.Application.Common.Mappings;
using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Jobs
{
    public class GetAppliedJobs
    {
        public class AppliedJobsVm
        {
            public IList<AppliedJobLookupDto> Jobs { get; set; }
        }

        public class AppliedJobLookupDto : IMapWith<Job>
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Location Location { get; set; }
            public DateTime DatePosted { get; set; }
            public string Employment { get; set; }
            public string ShortDiscription { get; set; }
            public Category Category { get; set; }
            public Employer Employer { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<Job, AppliedJobLookupDto>();
            }
        }

        public class GetAppliedJobsQuery : IRequest<AppliedJobsVm>
        {
            public Guid EmployeeId { get; set; }
        }

        public class GetAppliedJobsQueryHandler : IRequestHandler<GetAppliedJobsQuery, AppliedJobsVm>
        {
            public readonly IJobBoardDbContext _context;
            public readonly IMapper _mapper;

            public GetAppliedJobsQueryHandler(IJobBoardDbContext context,
                IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<AppliedJobsVm> Handle(GetAppliedJobsQuery request, CancellationToken cancellationToken)
            {
                var jobs = await _context.JobEmployees
                    .Include(x => x.Job)
                        .ThenInclude(x => x.Category)
                    .Include(x => x.Job)
                        .ThenInclude(x => x.Location)
                    .Where(x => x.EmployeeId == request.EmployeeId)
                    .Select(x => x.Job)
                    .ProjectTo<AppliedJobLookupDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return new AppliedJobsVm { Jobs = jobs };
            }
        }
    }
}