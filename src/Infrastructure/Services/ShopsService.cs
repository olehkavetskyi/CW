using Application.Dtos;
using Application.Extensions;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ShopsService : IShopsService
{
    private readonly CwContext _context;
    private readonly IEmployeesService _employeesService;

    public ShopsService(CwContext context, IEmployeesService employeesService)
    {
        _context = context;
        _employeesService = employeesService;
    }

    public async Task<Shop?> GetCurrentShopAsync(string email)
    {
        var currentEmployee = await _employeesService.GetEmployeeAsync(email);

        if (currentEmployee == null)
        {
            return null;
        }

        var currentShop = await _context
            .Shops
            .Include(s => s.Employees)
            .Where(x => x.Id == currentEmployee.ShopId)
            .FirstOrDefaultAsync();   

        return currentShop;
    }

    public async Task<double> GetAverageProductivityAsync(Guid id)
    {
        var shop = await _context
            .Shops
            .Include(s => s.Employees)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        double averageProductivity = 0;
        var employees = shop!.Employees;

        foreach (var employee in employees)
        {
            averageProductivity += _employeesService.GetProductivity(employee);
        }

        return Math.Round(averageProductivity / employees.Count, 2);
    }

    public async Task<List<ShopDto>> GetShopListAsync()
    {
        var shops = await _context
            .Shops
            .Include(s => s.Employees)
            .ToListAsync();

        var shopDtos = new List<ShopDto>();

        foreach(var shop in shops)
        {
            shopDtos.Add(shop.ToDto(await GetAverageProductivityAsync(shop.Id)));
        }

        return shopDtos;
    }
}
