namespace JobBoard.WebApi.Models
{
    public class FilterSortPaggingObject
    {
        public string? KeyWord { get; set; }
        public ICollection<Guid>? CategoryIds { get; set; }
        public ICollection<Guid>? LocationIds { get; set; }
        public int SalaryStart { get; set; }
        public int SalaryEnd { get; set; }
        public ICollection<Guid>? EmloyerIds { get; set; }
        public ICollection<int>? Experience { get; set; }
        public int Page { get; set; } = 1;
    }
}
