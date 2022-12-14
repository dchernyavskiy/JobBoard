namespace JobBoard.Domain
{
    public class Employer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AboutUs { get; set; }
        public int TeamSize { get; set; }
        public string Location { get; set; }
        public string PhotoLink { get; set; }
        public ICollection<Job> Jobs { get; set; }
        public bool IsBan { get; set; }
    }
}