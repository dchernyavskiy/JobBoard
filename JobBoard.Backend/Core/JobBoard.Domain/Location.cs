namespace JobBoard.Domain
{
    public class Location
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Job> Jobs { get; set; }
    }
}
