
using AutoMapper;
using JobBoard.Application.Common.Exceptions;
using JobBoard.Application.Common.Mappings;
using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Locations
{
    public class GetLocation
    {
        public class LocationVm : IMapWith<Location>
        {
            public Guid Id { get; set; }
            public string Name { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<Location, LocationVm>();
            }
        }

        public class GetLocationQuery : IRequest<LocationVm>
        {
            public Guid Id { get; set; }
        }

        public class GetLocationQueryHandler : IRequestHandler<GetLocationQuery, LocationVm>
        {

            private readonly IJobBoardDbContext _context;
            private readonly IMapper _mapper;

            public GetLocationQueryHandler(IJobBoardDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<LocationVm> Handle(GetLocationQuery request, CancellationToken cancellationToken)
            {
                var entity = await _context.Locations
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (entity == null)
                    throw new NotFoundException(nameof(Location), request.Id);

                return _mapper.Map<LocationVm>(entity);
            }
        }
    }
}
