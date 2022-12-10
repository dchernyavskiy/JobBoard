using JobBoard.Identity.Attributes;
using System.ComponentModel.DataAnnotations;

namespace JobBoard.Identity.Models
{
    public class RegisterEmployerViewModel : RegisterViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string AboutUs { get; set; }

        [Required]
        public int TeamSize { get; set; }

        [Required]
        public string Location { get; set; }

        [Available]
        [Required]
        public string PhotoLink { get; set; }
    }
}