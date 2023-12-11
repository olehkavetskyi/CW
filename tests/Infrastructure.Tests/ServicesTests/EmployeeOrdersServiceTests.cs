using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.ServicesTests;

public class EmployeeOrdersServiceTests
{
    [Fact]
    public async Task AddEmployeeOrderAsync_ShouldAddOrderAndAdjustQuantities()
    {
        // Arrange
        var email = "test@example.com";
        var contextOptions = new DbContextOptionsBuilder<CwContext>()
            .UseInMemoryDatabase(databaseName: "AddEmployeeOrderAsync_ShouldAddOrderAndAdjustQuantities")
            .Options;

        var context = new CwContext(contextOptions);
        var employeesServiceMock = new Mock<IEmployeesService>();
        employeesServiceMock.Setup(e => e.GetEmployeeAsync(email))
            .ReturnsAsync(new Employee { Id = Guid.NewGuid(), ShopId = Guid.NewGuid() });

        var employeeOrdersService = new EmployeeOrdersService(context, employeesServiceMock.Object);

        // Create test data
        var product1 = new Product { Id = Guid.NewGuid(), Name = "y23", Category = "bomb" };
        var product2 = new Product { Id = Guid.NewGuid(), Name = "y23", Category = "bomb" };
        var warehouseProduct1 = new WarehouseProduct { ProductId = product1.Id, Quantity = 10 };
        var warehouseProduct2 = new WarehouseProduct { ProductId = product2.Id, Quantity = 15 };
        var employeeOrderProducts = new List<EmployeeOrderProduct>
            {
                new EmployeeOrderProduct { ProductId = product1.Id, Quantity = 2 },
                new EmployeeOrderProduct { ProductId = product2.Id, Quantity = 3 },
            };

        context.Products.AddRange(product1, product2);
        context.WarehouseProducts.AddRange(warehouseProduct1, warehouseProduct2);
        context.SaveChanges();

        // Act
        await employeeOrdersService.AddEmployeeOrderAsync(employeeOrderProducts, email);

        // Assert
        var addedOrder = await context.EmployeeOrders.FirstOrDefaultAsync();
        Assert.NotNull(addedOrder);
        Assert.Equal(2, addedOrder.Products.Count);

        var updatedWarehouseProduct1 = await context.WarehouseProducts
            .FirstOrDefaultAsync(wp => wp.ProductId == product1.Id);
        var updatedWarehouseProduct2 = await context.WarehouseProducts
            .FirstOrDefaultAsync(wp => wp.ProductId == product2.Id);

        Assert.NotNull(updatedWarehouseProduct1);
        Assert.NotNull(updatedWarehouseProduct2);
        Assert.Equal(8, updatedWarehouseProduct1.Quantity); 
        Assert.Equal(12, updatedWarehouseProduct2.Quantity); 

        var shopProduct1 = await context.ShopProducts
            .FirstOrDefaultAsync(sp => sp.ProductId == product1.Id);
        var shopProduct2 = await context.ShopProducts
            .FirstOrDefaultAsync(sp => sp.ProductId == product2.Id);

        Assert.NotNull(shopProduct1);
        Assert.NotNull(shopProduct2);
        Assert.Equal(2, shopProduct1.Quantity); 
        Assert.Equal(3, shopProduct2.Quantity); 
    }
}