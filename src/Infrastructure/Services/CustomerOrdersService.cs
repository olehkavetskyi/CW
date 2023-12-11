using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Application.Extensions;
using Microsoft.EntityFrameworkCore;
using Application.Dtos;

namespace Infrastructure.Services;

public class CustomerOrdersService : ICustomerOrdersService
{
    private readonly CwContext _context;
    private readonly IShopsService _shopsService;
    private readonly IEmployeesService _employeesService;

    public CustomerOrdersService(CwContext context, 
        IShopsService shopsService, 
        IEmployeesService employeesService) 
    {
        _employeesService = employeesService;
        _context = context;
        _shopsService = shopsService;
    }

    public async Task<List<CustomerOrder>> GetCurrentCustomerOrdersAsync(string email)
    {
        var currentShop = await _shopsService.GetCurrentShopAsync(email);

        if (currentShop == null)
        {
            return new List<CustomerOrder>();
        }

        var customerOrders = await _context
            .CustomerOrders
            .Include(c => c.Products)
            .Where(e => e.ShopId == currentShop.Id)
            .ToListAsync();

        return customerOrders;
    }

    public async Task AddCustomerOrderAsync(CustomerOrderDto customerOrderDto, string email)
    {
        await SubtractQuantityFromShopAsync(customerOrderDto.Products);

        var currentEmployee = await _employeesService.GetEmployeeAsync(email);
        var customer = await _context.Employees.Where(e => e.EmailAddress ==  email).FirstOrDefaultAsync();
        Guid? customerId = null;

        if (customer != null)
        {
            customerId = customer.Id;
        }

        var customerOrder = customerOrderDto.Products.ToCustomerOrder(customerId, currentEmployee!.Id, (Guid)currentEmployee.ShopId!);

        await _context.CustomerOrders.AddRangeAsync(customerOrder);
        await _context.SaveChangesAsync();
    }

    public async Task SubtractQuantityFromShopAsync(List<CustomerOrderProduct> productsToRemove)
    {
        foreach (var productToRemove in productsToRemove)
        {
            var shopProduct = await _context.ShopProducts
                .FirstOrDefaultAsync(wp =>
                    wp.ProductId == productToRemove.ProductId);

            if (shopProduct != null)
            {
                if (productToRemove.Quantity > shopProduct.Quantity)
                {
                    throw new InvalidOperationException($"Insufficient quantity in shop for product with ID {productToRemove.ProductId}.");
                }

                shopProduct.Quantity -= productToRemove.Quantity;
            }
        }
    }
}
