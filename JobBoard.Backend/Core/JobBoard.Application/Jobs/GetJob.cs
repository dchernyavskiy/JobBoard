using AutoMapper;
using JobBoard.Application.Common.Exceptions;
using JobBoard.Application.Common.Mappings;
using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Jobs
{
    public class GetJob
    {
        public class JobVm : IMapWith<Job>
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

            //public Employer Employer { get; set; }
            public ICollection<Responsibility> Responsibilities { get; set; }

            public ICollection<Qualification> Qualifications { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<Job, JobVm>();
            }
        }

        public class GetJobQuery : IRequest<JobVm>
        {
            public Guid Id { get; set; }
        }

        public class GetJobQueryHandler : IRequestHandler<GetJobQuery, JobVm>
        {
            private readonly IJobBoardDbContext _context;
            private readonly IMapper _mapper;

            public GetJobQueryHandler(IJobBoardDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<JobVm> Handle(GetJobQuery request, CancellationToken cancellationToken)
            {
                var entity = await _context.Jobs
                    .Include(x => x.Employer)
                    .Include(x => x.Responsibilities)
                    .Include(x => x.Qualifications)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (entity == null)
                    throw new NotFoundException(nameof(Job), request.Id);

                return _mapper.Map<JobVm>(entity);
            }
        }
    }
}