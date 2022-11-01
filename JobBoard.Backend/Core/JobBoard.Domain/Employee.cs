using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoard.Domain
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Website { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string? Zip { get; set; } 
        public Guid? JobId { get; set; }
        public Job? Job { get; set; }
    }
}