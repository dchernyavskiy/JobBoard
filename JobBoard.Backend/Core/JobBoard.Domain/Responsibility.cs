namespace JobBoard.Domain
{
    public class Responsibility
    {
        public Guid Id { get; set; }
        public string Discription { get; set; }
        public Guid JobId { get; set; }
        public Job Job { get; set; }
    }
}
