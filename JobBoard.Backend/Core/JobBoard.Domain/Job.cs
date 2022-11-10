namespace JobBoard.Domain
{
    public class Job
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public string ShortDiscription { get; set; }
        public DateTime DatePosted { get; set; }
        public Guid LocationId { get; set; }
        public Location Location { get; set; }
        public int Hours { get; set; }
        public int SalaryStart { get; set; }
        public int SalaryEnd { get; set; }
        public int Experience { get; set; }
        public string Employment { get; set; }
        public Guid EmployerId { get; set; }
        public Employer Employer { get; set; }
        public Guid? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }
        public ICollection<Responsibility> Responsibilities { get; set; }
        public ICollection<Qualification> Qualifications { get; set; }

    }
}
