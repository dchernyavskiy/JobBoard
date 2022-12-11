namespace JobBoard.Domain
{
    public class JobEmployee
    {
        public Guid Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public Guid JobId { get; set; }
        public Job Job { get; set; }
    }
}