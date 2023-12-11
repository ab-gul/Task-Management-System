using Tasks.API.Data.Abstract;
using Tasks.API.Domain;

namespace Tasks.API.Data.Concrete
{
    public class TaskRepository : BaseRepository<TaskItem>, ITaskRepository
    {
        public TaskRepository(AppDbContext context) : base(context)
        {
        }
    }
}
