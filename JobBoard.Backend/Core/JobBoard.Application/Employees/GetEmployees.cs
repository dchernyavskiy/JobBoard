using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobBoard.Application.Common.Mappings;
using JobBoard.Application.Common.Objects;
using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Employees
{
    public class GetEmployees
    {
        public class JobsVm
        {
            public IList<JobLookupDto> Jobs { get; set; }
        }
        
        public class JobLookupDto : IMapWith<Job>
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Location Location { get; set; }
            public int SalaryStart { get; set; }
            public int SalaryEnd { get; set; }

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
                // 1. sort
                // 2. filter
                // 3. pagging
                var entities = _context.Jobs
                    .Include(x => x.Location)
                    .Skip((request.Pagging.Page - 1) * request.Pagging.Count)
                    .Take(request.Pagging.Count)
                    .Where(x => request.Filters.KeyWord == null ? true : request.Filters.KeyWord.Contains(x.Name))
                    .Where(x => request.Filters.CategoryIds == null ? true : request.Filters.CategoryIds.Contains(x.CategoryId))
                    .Where(x => request.Filters.LocationIds == null ? true : request.Filters.LocationIds.Contains(x.LocationId))
                    .Where(x => request.Filters.SalaryStart == 0 ? true : x.SalaryStart >= request.Filters.SalaryStart)
                    .Where(x => request.Filters.SalaryEnd == 0 ? true : x.SalaryEnd <= request.Filters.SalaryEnd)
                    .Where(x => request.Filters.EmloyerIds == null ? true : request.Filters.EmloyerIds.Contains(x.EmployerId))
                    .Where(x => request.Filters.Experiences == null ? true : request.Filters.Experiences.Contains(x.Experience));

                if (request != null && request.Sort.IsAscending)
                {
                    if (request.Sort.SortByName)
                        entities = entities.OrderBy(x => x.Name);
                    if (request.Sort.SortBySalary)
                        entities = entities.OrderBy(x => x.SalaryStart);
                    if (request.Sort.SortByExpirience)
                        entities = entities.OrderBy(x => x.Experience);
                }
                else if (request != null)
                {
                    if (request.Sort.SortByName)
                        entities = entities.OrderByDescending(x => x.Name);
                    if (request.Sort.SortBySalary)
                        entities = entities.OrderByDescending(x => x.SalaryStart);
                    if (request.Sort.SortByExpirience)
                        entities = entities.OrderByDescending(x => x.Experience);
                }

                var vms = await entities
                    .ProjectTo<JobLookupDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return new JobsVm { Jobs = vms };
            }
        }
    }
}
