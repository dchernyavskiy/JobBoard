using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Domain
{
    public class Education
    {
        public Guid Id{ get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string University { get; set; }
        public string Discription { get; set; }
    }
}
