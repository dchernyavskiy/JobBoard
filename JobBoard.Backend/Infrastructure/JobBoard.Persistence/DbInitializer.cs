namespace JobBoard.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(JobBoardDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
