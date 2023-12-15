namespace Tasks.WEB.Models
{
    public class TaskListDTO
    {
        public IEnumerable<TaskDTO> Items { get; set; } = new List<TaskDTO>();

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

    }
}
