using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobBoard.Application.Common.Exceptions;
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
using static JobBoard.Application.Jobs.GetJob;

namespace JobBoard.Application.Employers
{
    public class GetEmployer
    {
        public class EmployerVm : IMapWith<Employer>
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string AboutUs { get; set; }
            public int TeamSize { get; set; }
            public string Location { get; set; }
            public string PhotoLink { get; set; }
            public ICollection<Job> Jobs { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<Employer, EmployerVm>();
            }
        }

        public class GetEmployerQuery : IRequest<EmployerVm>
        {
            public Guid EmployerId { get; set; }
        }

        public class GetEmployerQueryHandler : IRequestHandler<GetEmployerQuery, EmployerVm>
        {
            private readonly IJobBoardDbContext _context;
            private readonly IMapper _mapper;

            public GetEmployerQueryHandler(IJobBoardDbContext context,
                IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<EmployerVm> Handle(GetEmployerQuery request, CancellationToken cancellationToken)
            {
                var employer = await _context.Employers
                    .Include(x => x.Jobs)
                    .ProjectTo<EmployerVm>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.Id == request.EmployerId);

                if (employer == null)
                    throw new NotFoundException(nameof(Employer), request.EmployerId);

                return employer;
            }
        }



    }
}
