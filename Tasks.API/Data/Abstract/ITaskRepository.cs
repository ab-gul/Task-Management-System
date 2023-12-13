using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Tasks.API.Domain;
using Tasks.API.Pagination;

namespace Tasks.API.Data.Abstract
{
    public interface ITaskRepository : IBaseRepository<TaskItem>
    {
        Task<PagedResponse<TResult>> GetPaginatedAsync<TResult>(
            PaginationFilter filter,
            Expression<Func<TaskItem, TResult>> projection);
    }
}           
