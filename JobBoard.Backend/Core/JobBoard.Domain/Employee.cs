using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoard.Domain
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string? Website { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string? Zip { get; set; } 
    }
}