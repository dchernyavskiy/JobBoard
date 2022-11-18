using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobBoard.Application.Common.Extensions;
using JobBoard.Application.Common.Mappings;
using JobBoard.Application.Common.Objects;
using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Jobs
{
    public class GetJobs
    {
        public class JobsVm
        {
            public IList<JobLookupDto> Jobs { get; set; }
            public int PageCount { get; set; }
        }
        public class JobLookupDto : IMapWith<Job>
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Location Location { get; set; }
            public DateTime DatePosted { get; set; }
            public string Employment { get; set; }
            public string ShortDiscription { get; set; }
            public Category Category { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<Job, JobLookupDto>();
            }
        }

        public class GetJobsQuery : IRequest<JobsVm>
        {
            public JobFilter Filters { get; set; } = new JobFilter
            {
                CategoryIds = null,
                EmloyerIds = null,
                Experiences = null,
                SalaryStart = 0,
                SalaryEnd = 0,
                KeyWord = null,
                LocationIds = null
            };
            public Pagging Pagging { get; set; } = new Pagging
            {
                Count = 12,
                Page = 1
            };
            public JobSort Sort { get; set; } = new JobSort
            {
                IsAscending = true,
                SortByName = false,
                SortByExpirience = false,
                SortBySalary = false
            };

        }

        public class GetJobsQueryHandler : IRequestHandler<GetJobsQuery, JobsVm>
        {

            private readonly IJobBoardDbContext _context;
            private readonly IMapper _mapper;

            public GetJobsQueryHandler(IJobBoardDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<JobsVm> Handle(GetJobsQuery request, CancellationToken cancellationToken)
            {
                var entities = _context.Jobs
                    .Include(x => x.Location)
                    .Include(x => x.Category) as IQueryable<Job>;

                var count = entities.Count();

                if (request != null)
                {
                    if (request.Sort.SortByName)
                        entities = entities.OrderBy(x => x.Name, request.Sort.IsAscending);
                    if (request.Sort.SortBySalary)
                        entities = entities.OrderBy(x => x.SalaryStart, request.Sort.IsAscending);
                    if (request.Sort.SortByExpirience)
                        entities = entities.OrderBy(x => x.Experience, request.Sort.IsAscending);

                    entities = entities
                           .Where(x => request.Filters.KeyWord == null ? true : request.Filters.KeyWord.Contains(x.Name))
                           .Where(x => request.Filters.CategoryIds == null ? true : request.Filters.CategoryIds.Contains(x.CategoryId))
                           .Where(x => request.Filters.LocationIds == null ? true : request.Filters.LocationIds.Contains(x.LocationId))
                           .Where(x => request.Filters.SalaryStart == 0 ? true : x.SalaryStart >= request.Filters.SalaryStart)
                           .Where(x => request.Filters.SalaryEnd == 0 ? true : x.SalaryEnd <= request.Filters.SalaryEnd)
                           .Where(x => request.Filters.EmloyerIds == null ? true : request.Filters.EmloyerIds.Contains(x.EmployerId))
                           .Where(x => request.Filters.Experiences == null ? true : request.Filters.Experiences.Contains(x.Experience))
                           .Skip((request.Pagging.Page - 1) * request.Pagging.Count)
                           .Take(request.Pagging.Count);
                }

                var jobs = await entities
                    .ProjectTo<JobLookupDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                var jobsCount = await _context.Jobs.CountAsync();
                var requestJobsCount = (double)request.Pagging.Count;
                var pageCount = (int)Math.Ceiling(jobsCount / requestJobsCount);

                return new JobsVm { Jobs = jobs, PageCount = pageCount };
            }
        }
    }
}
