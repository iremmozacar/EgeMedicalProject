using System.Linq.Expressions;
using System.Runtime.Intrinsics.Arm;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using EgeApp.Backend.Data.Abstract;

namespace EgeApp.Backend.Data.Concrete.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? options = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? predicate = null)
        {
            IQueryable<TEntity> query = _dbSet; 
            if (predicate != null)
            {
                query = predicate(query);
            }
            if (options != null)
            {
                query = query.Where(options);
            }
            return await query.ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>>? options = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? predicate = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (predicate != null)
            {
                query = predicate(query);
            }
            if (options != null)
            {
                query = query.Where(options);
            }

#pragma warning disable CS8603 
            return await query.AsNoTracking().SingleOrDefaultAsync();
#pragma warning restore CS8603 

        }


        public async Task<TEntity> GetFirstAsync(
            Expression<Func<TEntity, bool>>? options = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            IQueryable<TEntity> query = _dbSet;

    
            if (include != null)
            {
                query = include(query);
            }

           
            if (options != null)
            {
                query = query.Where(options);
            }

         
            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>>? options = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? predicate = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (predicate != null)
            {
                query = predicate(query);
            }
            if (options != null)
            {
                query = query.Where(options);
            }
            return await query.CountAsync();
        }
        public async Task<List<TEntity>> GetHomeAsync()
        {
            var property = typeof(TEntity).GetProperty("IsHome");
            if (property == null)
            {
                throw new InvalidOperationException($"{typeof(TEntity).Name} entity'sinde 'IsHome' özelliği bulunamadı.");
            }

            return await _dbSet.Where(e => EF.Property<bool>(e, "IsHome")).ToListAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<(List<TEntity>, int)> GetPagedAsync(
    int pageIndex, int pageSize,
    Expression<Func<TEntity, bool>>? filter = null,
    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (include != null)
            {
                query = include(query);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var totalCount = await query.CountAsync();
            var pagedData = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return (pagedData, totalCount);
        }
        public async Task<List<TEntity>> GetSortedAsync<TKey>(
            Expression<Func<TEntity, TKey>> orderBy,
            bool isDescending = false,
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (include != null)
            {
                query = include(query);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = isDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);

            return await query.ToListAsync();
        }
        public async Task<List<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? options = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? predicate = null,
            bool asNoTracking = false)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
            {
                query = predicate(query);
            }

            if (options != null)
            {
                query = query.Where(options);
            }

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }

    }
}

