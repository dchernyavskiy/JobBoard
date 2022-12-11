namespace JobBoard.WebApi.Models
{
    public class UpdateEmployeeCommandDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CVLink { get; set; }
    }
}
