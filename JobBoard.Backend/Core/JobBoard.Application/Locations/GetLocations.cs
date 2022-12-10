using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Locations
{
    public class GetLocations
    {
        public class LocationsVm
        {
            public IList<Location> Locations { get; set; }
        }

        public class GetLocationsQuery : IRequest<LocationsVm>
        { }

        public class GetLocationsQueryHandler : IRequestHandler<GetLocationsQuery, LocationsVm>
        {
            private readonly IJobBoardDbContext _context;

            public GetLocationsQueryHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<LocationsVm> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
            {
                return new LocationsVm { Locations = await _context.Locations.ToListAsync() };
            }
        }
    }
}