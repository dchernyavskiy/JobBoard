using AutoMapper;
using JobBoard.Application.Common.Mappings;
using static JobBoard.Application.Jobs.CreateJob;

namespace JobBoard.WebApi.Models
{
    public class CreateJobCommandDto : IMapWith<CreateJobCommand>
    {
        public string Name { get; set; }
        public string Discription { get; set; }
        public string Location { get; set; }
        public int Hours { get; set; }
        public int SalaryStart { get; set; }
        public int SalaryEnd { get; set; }
        public int Experience { get; set; }
        public Guid CategoryId { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateJobCommandDto, CreateJobCommand>();
        }
    }
}