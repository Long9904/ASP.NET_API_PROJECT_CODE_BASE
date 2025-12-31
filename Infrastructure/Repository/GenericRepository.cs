using System.Linq.Expressions;
using Domain.Interface.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }


        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }


        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.CountAsync(cancellationToken);
        }


        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(filter, cancellationToken);
        }


        public async Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            int? pageIndex = null, int? pageSize = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null) query = query.Where(filter);
            if (include != null) query = include(query);

            // Pagination
            if (pageIndex.HasValue && pageSize.HasValue)
            {
                int skip = (pageIndex.Value - 1) * pageSize.Value;
                query = query.Skip(skip).Take(pageSize.Value);
            }

            return await query.ToListAsync(cancellationToken);
        }


        public async Task<T?> GetAsync(
                    Expression<Func<T, bool>> filter,
                    Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                    CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;
            if (include != null) query = include(query);

            return await query.FirstOrDefaultAsync(filter, cancellationToken);
        }


        public async Task RemoveByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.FindAsync(new[] { id }, cancellationToken);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }


        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
