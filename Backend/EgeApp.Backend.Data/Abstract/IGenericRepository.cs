using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace EgeApp.Backend.Data.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
      
        Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>>? options = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? predicate = null
        );
        Task<List<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? options = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? predicate = null
        );
        Task<int> GetCountAsync(
            Expression<Func<TEntity, bool>>? options = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? predicate = null

        );
        
        Task<List<TEntity>> GetHomeAsync();

      
        Task<(List<TEntity>, int)> GetPagedAsync(
            int pageIndex, int pageSize,
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

     
        Task<List<TEntity>> GetSortedAsync<TKey>(
            Expression<Func<TEntity, TKey>> orderBy,
            bool isDescending = false,
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

      
        Task AddRangeAsync(IEnumerable<TEntity> entities);

      
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
    }
}

