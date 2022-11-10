using JobBoard.Identity.Attributes;
using System.ComponentModel.DataAnnotations;

namespace JobBoard.Identity.Models
{
    public class RegisterEmployeeViewModel : RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Available]
        public string CVLink { get; set; }
    }
}
