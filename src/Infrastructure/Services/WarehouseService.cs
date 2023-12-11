using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class WarehouseService : IWarehouseService
{
    private readonly CwContext _context;

    public WarehouseService(CwContext context)
    {
        _context = context;
    }

    public async Task<Warehouse> GetWarehouseAsync()
    {
        return await _context
            .Warehouses
            .Include(w => w.WarehouseProducts)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync();
    }
}
