using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Domain
{
    public class Employer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AboutUs { get; set; }
        public string Responsibilities { get; set; }
        public ICollection<Job> Jobs { get; set; }
    }
}
