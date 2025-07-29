using EVABookShopAPI.Repository.GenericRepository;
using System.Data;

namespace EVABookShopAPI.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChanges();

        IGenericRepository<TEntity> Repository<TEntity>()
            where TEntity : class;

        Task BeginTransactionAsync(IsolationLevel isolationLevel);

        Task CommitTransactionAsync();

        Task RollbackTransactionAsync();
    }
}
