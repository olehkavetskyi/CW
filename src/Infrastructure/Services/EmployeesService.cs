using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class EmployeesService : IEmployeesService
{
    private readonly CwContext _context;

    public EmployeesService(CwContext context)
    {
        _context = context;
    }

    public async Task<Employee?> GetEmployeeAsync(string email)
    {

        if (email == null)
        {
            return null;
        }

        var currentEmployee = await _context
            .Employees
            .Where(x => x.EmailAddress == email)
            .FirstOrDefaultAsync();

        return currentEmployee;
    }

    public double GetProductivity(Employee employee)
    {
        var totalEmployeeOrderPrice = _context.EmployeeOrders
            .Where(eo => eo.EmployeeId == employee.Id)
            .Include(eo => eo.Products)
            .SelectMany(eo => eo.Products)
            .Join(
                _context.WarehouseProducts,
                employeeProduct => employeeProduct.ProductId,
                warehouseProduct => warehouseProduct.ProductId,
                (employeeProduct, warehouseProduct) => new { employeeProduct, warehouseProduct })
            .Join(
                _context.Products,
                combined => combined.warehouseProduct.ProductId,
                product => product.Id,
                (combined, product) => new { combined.employeeProduct, product })
            .Sum(combined => combined.product.Price * combined.employeeProduct.Quantity);

        var totalShopRevenue = _context.CustomerOrders
                .Where(co => co.EmployeeId == employee.Id)
                .Include(co => co.Products)
                .SelectMany(co => co.Products)
                .Join(
                    _context.ShopProducts,
                    customerProduct => customerProduct.ProductId,
                    shopProduct => shopProduct.ProductId,
                    (customerProduct, shopProduct) => new { customerProduct, shopProduct })
                .Join(
                    _context.Products,
                    combined => combined.shopProduct.ProductId,
                    product => product.Id,
                    (combined, product) => new { combined.customerProduct, product })
        .Sum(combined => combined.product.Price * combined.customerProduct.Quantity);

        if (totalEmployeeOrderPrice == 0) 
        {
            return 0;
        }

        return Math.Round((double)(totalShopRevenue / totalEmployeeOrderPrice) * 100, 2);
    }
}
