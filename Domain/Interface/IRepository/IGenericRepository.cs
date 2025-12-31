using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Domain.Interface.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetAsync(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            CancellationToken cancellationToken = default); // Thêm ở đây

        Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            int? pageIndex = null,
            int? pageSize = null,
            CancellationToken cancellationToken = default); // Thêm ở đây

        Task AddAsync(T entity, CancellationToken cancellationToken = default);

        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        Task RemoveByIdAsync(object id, CancellationToken cancellationToken = default);

        void Update(T entity);

        Task<int> CountAsync(CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(
            Expression<Func<T, bool>> filter, 
            CancellationToken cancellationToken = default);
    }
}
