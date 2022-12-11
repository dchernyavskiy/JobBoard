using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobBoard.Application.Common.Mappings;
using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Employers
{
    public class GetEmployers
    {
        public class EmployersVm
        {
            public IList<EmployerLookupDto> Employers { get; set; }
        }

        public class EmployerLookupDto : IMapWith<Employer>
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string AboutUs { get; set; }
            public string Location { get; set; }
            public string PhotoLink { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<Employer, EmployerLookupDto>();
            }
        }

        public class GetEmployersQuery : IRequest<EmployersVm>
        {
        }

        public class GetEmployersQueryhandler : IRequestHandler<GetEmployersQuery, EmployersVm>
        {
            private readonly IJobBoardDbContext _context;
            private readonly IMapper _mapper;

            public GetEmployersQueryhandler(IJobBoardDbContext context,
                IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<EmployersVm> Handle(GetEmployersQuery request, CancellationToken cancellationToken)
            {
                var employers = await _context.Employers
                    .ProjectTo<EmployerLookupDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return new EmployersVm { Employers = employers };
            }
        }
    }
}