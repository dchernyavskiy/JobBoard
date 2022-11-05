using AutoMapper;
using JobBoard.Application.Common.Mappings;
using static JobBoard.Application.Educations.CreateEducation;

namespace JobBoard.WebApi.Models
{
    public class CreateEducationCommandDto : IMapWith<CreateEducationCommand>
    {
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string University { get; set; }
        public string Discription { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateEducationCommandDto, CreateEducationCommand>();
        }
    }
}
