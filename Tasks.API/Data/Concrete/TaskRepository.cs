using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tasks.API.Data.Abstract;
using Tasks.API.Domain;
using Tasks.API.Pagination;

namespace Tasks.API.Data.Concrete
{
    public class TaskRepository : BaseRepository<TaskItem>, ITaskRepository
    {
        public TaskRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<PagedResponse<TResult>> GetPaginatedAsync<TResult>(
            PaginationFilter filter,
            Expression<Func<TaskItem, TResult>> projection)
        {
            ArgumentNullException.ThrowIfNull(nameof(filter));
            ArgumentNullException.ThrowIfNull(nameof(projection));

            IQueryable<TaskItem> query = _entities
                  .OrderBy(e => e.Id)
                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                  .Take(filter.PageSize)
                  .AsNoTracking();

                var tasks = await query.Select(projection).ToListAsync() ?? new List<TResult>();

                var totalCount = await _entities.CountAsync();

                return PagedResponse<TResult>.Create(tasks, filter.PageNumber, filter.PageSize, totalCount);
            }
        }
}
