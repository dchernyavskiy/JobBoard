namespace JobBoard.Application.Common.Objects
{
    public class JobSort
    {
        public bool SortByName { get; set; }
        public bool SortBySalary { get; set; }
        public bool SortByExpirience { get; set; }
        public bool IsAscending { get; set; } = true;
    }
}