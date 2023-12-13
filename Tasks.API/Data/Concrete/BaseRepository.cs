using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Tasks.API.Data.Abstract;
using Tasks.API.Domain;
using Tasks.API.Pagination;

namespace Tasks.API.Data.Concrete
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : Base
    {
        protected readonly AppDbContext _dbContext;
        protected readonly DbSet<T> _entities;
        public BaseRepository(AppDbContext context)
        {
            _dbContext = context;
            _entities = context.Set<T>();
        }
        public async Task<T> CreateAsync(T entity)
        {
            _entities.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return (await _entities.Where(e => e.Id == id).ExecuteDeleteAsync()) switch
            {
                1 => true,
                _ => false
            };
        }

        public  async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.AsNoTracking().ToListAsync() ?? new List<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _entities.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(T entity)
        {
            _entities.Update(entity);
            await _dbContext.SaveChangesAsync();
        }


    }
}
