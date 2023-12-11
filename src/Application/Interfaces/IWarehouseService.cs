using Domain.Entities;

namespace Application.Interfaces;

public interface IWarehouseService
{
    Task<Warehouse> GetWarehouseAsync();
}
