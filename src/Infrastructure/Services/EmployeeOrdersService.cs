using Application.Extensions;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class EmployeeOrdersService : IEmployeeOrdersService
{
    private readonly CwContext _context;
    private readonly IEmployeesService _employeesService;

    public EmployeeOrdersService(CwContext context, IEmployeesService employeesService)
    {
        _context = context;
        _employeesService = employeesService;
    }

    public async Task AddEmployeeOrderAsync(List<EmployeeOrderProduct> employeeOrderProducts, string email)
    {
        await SubtractQuantityFromWarehouseAsync(employeeOrderProducts);

        var currentEmployee = await _employeesService.GetEmployeeAsync(email);
        var employeeOrder = employeeOrderProducts.ToEmployeeOrder(currentEmployee!.Id);

        await _context.EmployeeOrders.AddRangeAsync(employeeOrder);
        await AddQuantityToShopAsync(employeeOrder, currentEmployee);
        await _context.SaveChangesAsync();
    }

    public async Task AddQuantityToShopAsync(EmployeeOrder employeeOrder, Employee employee)
    {
        foreach (var product in employeeOrder.Products)
        {
            if (await _context.ShopProducts.AnyAsync(s => s.ProductId == product.ProductId))
            {
                await _context
                    .ShopProducts
                    .Where(s => s.ProductId == product.ProductId)
                    .ExecuteUpdateAsync(
                        setters => setters.SetProperty(
                            p => p.Quantity, 
                            p => p.Quantity + product.Quantity
                            )
                        );
            }
            else
            {
                await _context
                    .ShopProducts
                    .AddAsync(product.ToShopProduct((Guid)employee.ShopId!));
            }
        }
    }

    public async Task SubtractQuantityFromWarehouseAsync(List<EmployeeOrderProduct> productsToRemove)
    {
        foreach (var productToRemove in productsToRemove)
        {
            var warehouseProduct = await _context.WarehouseProducts
                .FirstOrDefaultAsync(wp =>
                    wp.ProductId == productToRemove.ProductId);

            if (warehouseProduct != null)
            {
                if (productToRemove.Quantity > warehouseProduct.Quantity)
                {
                    throw new InvalidOperationException($"Insufficient quantity in warehouse for product with ID {productToRemove.ProductId}.");
                }

                warehouseProduct.Quantity -= productToRemove.Quantity;
            }
        }
    }

    public async Task<List<EmployeeOrder>> GetCurrentEmployeeOrdersAsync(string email)
    {
        var currentEmployee = await _employeesService.GetEmployeeAsync(email);

        if (currentEmployee == null)
        {
            return new List<EmployeeOrder>();
        }

        var employeeOrders = await _context
            .EmployeeOrders
            .Where(e => e.EmployeeId == currentEmployee.Id)
            .ToListAsync();

        return employeeOrders;
    }
}
