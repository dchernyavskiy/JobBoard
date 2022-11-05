using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobBoard.Application.Common.Mappings;
using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Application.Jobs
{
    public class GetJobs
    {
        public class JobsVm
        {
            public IList<JobLookupDto> Jobs { get; set; }
        }
        public class JobLookupDto : IMapWith<Job>
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Location { get; set; }
            public int SalaryStart { get; set; }
            public int SalaryEnd { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<Job, JobLookupDto>();
            }
        }

        public class GetJobsQuery : IRequest<JobsVm> { }

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
                var entities = await _context.Jobs
                    .ProjectTo<JobLookupDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return new JobsVm { Jobs = entities };
            }
        }
    }
}
