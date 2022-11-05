using AutoMapper;
using JobBoard.Application.Common.Mappings;
using JobBoard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JobBoard.Application.Stores.CreateJob;
using static JobBoard.Application.Stores.GetJob;

namespace JobBoard.WebApi.Models
{
    public class CreateJobCommandDto : IMapWith<CreateJobCommand>
    {
        public string Name { get; set; }
        public string Discription { get; set; }
        public DateTime DatePosted { get; set; }
        public string Location { get; set; }
        public int Hours { get; set; }
        public int SalaryStart { get; set; }
        public int SalaryEnd { get; set; }
        public int Experience { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateJobCommandDto, CreateJobCommand>();
        }
    }
}
