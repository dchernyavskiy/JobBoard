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
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
