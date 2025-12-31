using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.Interface.IRepository
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);

        void Dispose();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<IDbContextTransaction> BeginTransactionAsync();


        //-----------------------//
        public IUserRepository UserRepository { get; }
    }
}
