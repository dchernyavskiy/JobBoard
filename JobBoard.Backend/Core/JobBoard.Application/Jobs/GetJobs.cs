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
            public int ResultCount { get; set; }
        }

        public class JobLookupDto : IMapWith<Job>
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Location Location { get; set; }
            public DateTime DatePosted { get; set; }
            public string Employment { get; set; }
            public Category Category { get; set; }
            public Employer Employer { get; set; }
            public string Discription { get; set; }

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
                LocationIds = null,
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
                    .Include(x => x.Employer)
                    .Include(x => x.Category)
                    .Where(x => !x.Employer.IsBan);

                var resultCount = 0;

                var requestJobsCount = 0d;
                var pageCount = 0;
                if (request != null)
                {
                    requestJobsCount = (double)request.Pagging.Count;

                    if (request.Sort.SortByName)
                        entities = entities.OrderBy(x => x.Name, request.Sort.IsAscending);
                    if (request.Sort.SortBySalary)
                        entities = entities.OrderBy(x => x.SalaryStart, request.Sort.IsAscending);
                    if (request.Sort.SortByExpirience)
                        entities = entities.OrderBy(x => x.Experience, request.Sort.IsAscending);

                    entities = entities
                           .Where(x => string.IsNullOrEmpty(request.Filters.KeyWord) || x.Name.ToUpper().Contains(request.Filters.KeyWord.ToUpper()))
                           .Where(x => request.Filters.CategoryIds == null || request.Filters.CategoryIds.Count == 0 || request.Filters.CategoryIds.Contains(x.CategoryId))
                           .Where(x => request.Filters.LocationIds == null || request.Filters.LocationIds.Count == 0 || request.Filters.LocationIds.Contains(x.LocationId))
                           .Where(x => request.Filters.SalaryStart == 0 || x.SalaryStart >= request.Filters.SalaryStart)
                           .Where(x => request.Filters.SalaryEnd == 0 || x.SalaryEnd <= request.Filters.SalaryEnd)
                           .Where(x => request.Filters.EmloyerIds == null || request.Filters.EmloyerIds.Count == 0 || request.Filters.EmloyerIds.Contains(x.EmployerId))
                           .Where(x => request.Filters.Experiences == null || request.Filters.Experiences.Count == 0 || request.Filters.Experiences.Contains(x.Experience));

                    resultCount = entities.Count();

                    pageCount = (int)Math.Ceiling(resultCount / requestJobsCount);

                    entities = entities
                               .Skip((request.Pagging.Page - 1) * request.Pagging.Count)
                               .Take(request.Pagging.Count);
                }

                var jobs = await entities
                    .ProjectTo<JobLookupDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return new JobsVm { Jobs = jobs, PageCount = pageCount, ResultCount = resultCount };
            }
        }
    }
}