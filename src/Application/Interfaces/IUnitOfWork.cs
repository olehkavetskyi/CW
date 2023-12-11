using Domain.Entities;

namespace Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<int> CompleteAsync();
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
}
