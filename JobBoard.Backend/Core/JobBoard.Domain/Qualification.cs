using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Domain
{
    public class Qualification
    {
        public Guid Id { get; set; }
        public string Discription { get; set; }
        public Guid JobId { get; set; }
        public Job Job { get; set; }
    }
}
