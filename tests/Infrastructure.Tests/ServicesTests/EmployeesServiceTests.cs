using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.ServicesTests;
public class EmployeesServiceTests
{
    [Fact]
    public async Task GetEmployeeAsync_ShouldReturnEmployee()
    {
        // Arrange
        var email = "test@example.com";
        var contextOptions = new DbContextOptionsBuilder<CwContext>()
            .UseInMemoryDatabase(databaseName: "GetEmployeeAsync_ShouldReturnEmployee")
            .Options;

        var context = new CwContext(contextOptions);
        var employeesService = new EmployeesService(context);

        // Create test data
        var employee = new Employee { Id = Guid.NewGuid(), EmailAddress = email, FirstName = "o", LastName = "ui", Position = "Admin" };
        await context.Employees.AddAsync(employee);
        await context.SaveChangesAsync();

        // Act
        var result = await employeesService.GetEmployeeAsync(email);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(employee.Id, result?.Id);
    }

    [Fact]
    public async Task GetEmployeeAsync_ShouldReturnNullForNullEmail()
    {
        // Arrange
        var contextOptions = new DbContextOptionsBuilder<CwContext>()
            .UseInMemoryDatabase(databaseName: "GetEmployeeAsync_ShouldReturnNullForNullEmail")
            .Options;

        var context = new CwContext(contextOptions);
        var employeesService = new EmployeesService(context);

        // Act
        var result = await employeesService.GetEmployeeAsync(null);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetProductivity_ShouldReturnProductivity()
    {
        // Arrange
        var contextOptions = new DbContextOptionsBuilder<CwContext>()
            .UseInMemoryDatabase(databaseName: "GetProductivity_ShouldReturnProductivity")
            .Options;

        var context = new CwContext(contextOptions);
        var employeesService = new EmployeesService(context);

        // Create test data
        var employee = new Employee { Id = Guid.NewGuid(), Position = "Something", LastName = "last", FirstName = "first", EmailAddress = "Adress" };
        var product1 = new Product { Id = Guid.NewGuid(), Price = 10.0m, Category = "Catef", Name = "super" };
        var product2 = new Product { Id = Guid.NewGuid(), Price = 20.0m, Category = "Catef", Name = "super new" };
        var warehouseProduct = new WarehouseProduct { ProductId = product1.Id, Quantity = 5 };
        var shopProduct = new ShopProduct { ProductId = product2.Id, Quantity = 3 };
        var employeeOrderProduct = new EmployeeOrderProduct { ProductId = product1.Id, Quantity = 2 };
        var customerOrderProduct = new CustomerOrderProduct { ProductId = product2.Id, Quantity = 1 };
        var employeeOrder = new EmployeeOrder { EmployeeId = employee.Id, Products = [ employeeOrderProduct ] };
        var customerOrder = new CustomerOrder { EmployeeId = employee.Id, Products = [ customerOrderProduct ] };
 

        context.Products.AddRange(product1, product2);
        context.WarehouseProducts.Add(warehouseProduct);
        context.ShopProducts.Add(shopProduct);
        context.EmployeeOrders.Add(employeeOrder);
        context.CustomerOrders.Add(customerOrder);
        context.SaveChanges();

        // Act
        var result = employeesService.GetProductivity(employee);

        // Assert
        Assert.Equal(100, result, 2);
    }
}
