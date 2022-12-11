using AutoMapper;
using JobBoard.Application.Common.Mappings;
using static JobBoard.Application.Locations.CreateLocation;

namespace JobBoard.WebApi.Models
{
    public class CreateLocationCommandDto : IMapWith<CreateLocationCommand>
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateLocationCommandDto, CreateLocationCommand>();
        }
    }
}