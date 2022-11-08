using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Identity.Models
{
    public class RegisterEmployerViewModel : RegisterViewModel
    {
        public string Name { get; set; }
        public string AboutUs { get; set; }
        public string Responsibilities { get; set; }
        public int TeamSize { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public DateTime? Foundation { get; set; }
    }
}
