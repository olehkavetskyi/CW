using Domain.Entities;

namespace Application.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
}
