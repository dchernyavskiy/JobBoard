namespace JobBoard.Application.Common.Objects
{
    public class JobFilter
    {
        public string? KeyWord { get; set; }
        public ICollection<Guid>? CategoryIds { get; set; }
        public ICollection<Guid>? LocationIds { get; set; }
        public int SalaryStart { get; set; }
        public int SalaryEnd { get; set; }
        public ICollection<Guid>? EmloyerIds { get; set; }
        public ICollection<int>? Experiences { get; set; }
    }
}
