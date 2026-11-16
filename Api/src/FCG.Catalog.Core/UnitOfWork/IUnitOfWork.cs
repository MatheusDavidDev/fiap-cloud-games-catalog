using FCG.Catalog.Core.Repository;

namespace FCG.Catalog.Core.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChanges();

    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
}
