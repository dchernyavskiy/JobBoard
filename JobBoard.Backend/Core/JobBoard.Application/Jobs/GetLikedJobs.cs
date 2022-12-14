using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobBoard.Application.Common.Mappings;
using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Jobs
{
    public class GetLikedJobs
    {
        public class LikedJobsVm
        {
            public IList<LikedJobLookupDto> Jobs { get; set; }
        }

        public class LikedJobLookupDto : IMapWith<Job>
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
                profile.CreateMap<Job, LikedJobLookupDto>();
            }
        }

        public class GetLikedJobsQuery : IRequest<LikedJobsVm>
        {
            public Guid EmployeeId { get; set; }
        }

        public class GetLikedJobsQueryHandler : IRequestHandler<GetLikedJobsQuery, LikedJobsVm>
        {
            private readonly IJobBoardDbContext _context;
            private readonly IMapper _mapper;

            public GetLikedJobsQueryHandler(IJobBoardDbContext context,
                IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<LikedJobsVm> Handle(GetLikedJobsQuery request, CancellationToken cancellationToken)
            {
                var a = await _context.EmployeeLikeJobs
                    .Include(x => x.Job)
                        .ThenInclude(x => x.Employer)
                    .Where(x => x.EmployeeId == request.EmployeeId)
                    .Select(x => x.Job)
                    .ToListAsync();

                var jobs = await _context.EmployeeLikeJobs
                    .Include(x => x.Job)
                        .ThenInclude(x => x.Employer)
                    .Where(x => x.EmployeeId == request.EmployeeId)
                    .Select(x => x.Job)
                    .ProjectTo<LikedJobLookupDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return new LikedJobsVm { Jobs = jobs };
            }
        }
    }
}