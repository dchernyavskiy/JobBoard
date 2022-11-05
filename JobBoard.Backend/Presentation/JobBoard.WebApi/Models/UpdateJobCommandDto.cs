using JobBoard.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JobBoard.Application.Jobs.UpdateJob;

namespace JobBoard.WebApi.Models
{
    public class UpdateJobCommandDto : IMapWith<UpdateJobCommand>
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
    }
}
