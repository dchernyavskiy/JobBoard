namespace JobBoard.Domain
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CVLink { get; set; }
        public bool IsBan { get; set; }
        public ICollection<JobEmployee> AppliedJobs { get; set; }
    }
}