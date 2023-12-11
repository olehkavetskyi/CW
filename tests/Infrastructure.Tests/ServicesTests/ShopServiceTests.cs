using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.ServicesTests;
public class ShopsServiceTests
{
    [Fact]
    public async Task GetCurrentShopAsync_ShouldReturnCurrentShop()
    {
        // Arrange
        var employeeEmail = "test@example.com";
        var employee = new Employee { Id = Guid.NewGuid(), ShopId = Guid.NewGuid(), EmailAddress = "adress", FirstName = "first", LastName = "last", Position = "post", Salary = 2010 };
        var shop = new Shop { Id = (Guid)employee.ShopId, Address = "okn", Name = "hb", Employees = new List<Employee> { employee } };

        var employeesServiceMock = new Mock<IEmployeesService>();
        employeesServiceMock.Setup(s => s.GetEmployeeAsync(It.IsAny<string>())).ReturnsAsync(employee);

        var contextOptions = new DbContextOptionsBuilder<CwContext>()
            .UseInMemoryDatabase(databaseName: "GetCurrentShopAsync_ShouldReturnCurrentShop")
            .Options;
        var context = new CwContext(contextOptions);
        context.Employees.Add(employee);
        context.Shops.Add(shop);
        await context.SaveChangesAsync();

        var shopsService = new ShopsService(context, employeesServiceMock.Object);

        // Act
        var result = await shopsService.GetCurrentShopAsync(employeeEmail);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Shop>(result);
        Assert.Equal(employee.ShopId, result.Id);
    }

    [Fact]
    public async Task GetAverageProductivityAsync_ShouldReturnAverageProductivity()
    {
        // Arrange
        var shopId = Guid.NewGuid();
        var employee1 = new Employee { Id = Guid.NewGuid(), ShopId = shopId, EmailAddress = "adress", FirstName = "first", LastName = "last", Position = "post", Salary = 2010 };
        var employee2 = new Employee { Id = Guid.NewGuid(), ShopId = shopId, EmailAddress = "adress", FirstName = "first", LastName = "last", Position = "post", Salary = 2010 };
        var shop = new Shop { Id = shopId, Employees = new List<Employee> { employee1, employee2 }, Name = "name", Address = "ar" };

        var employeesServiceMock = new Mock<IEmployeesService>();
        employeesServiceMock.Setup(s => s.GetProductivity(It.IsAny<Employee>())).Returns(10.0);

        var contextOptions = new DbContextOptionsBuilder<CwContext>()
            .UseInMemoryDatabase(databaseName: "GetAverageProductivityAsync_ShouldReturnAverageProductivity")
            .EnableSensitiveDataLogging()  
            .Options;

        var context = new CwContext(contextOptions);
        context.Employees.AddRange(employee1, employee2);
        context.Shops.Add(shop);
        await context.SaveChangesAsync();

        var shopsService = new ShopsService(context, employeesServiceMock.Object);

        // Act
        var result = await shopsService.GetAverageProductivityAsync(shopId);

        // Assert
        Assert.Equal(10.0, result);
    }

    [Fact]
    public async Task GetShopListAsync_ShouldReturnShopListWithAverageProductivity()
    {
        // Arrange
        var firstShopId = Guid.NewGuid();
        var secondShopId = Guid.NewGuid();
        var shop1 = new Shop { Id = firstShopId, Address = "address1", Name = "Name", Employees = new List<Employee> { new Employee { Id = Guid.NewGuid(), EmailAddress = "adress", ShopId = firstShopId, FirstName = "first", LastName = "last", Position = "post", Salary = 2010 } } };
        var shop2 = new Shop { Id = secondShopId, Address = "address2", Name = "Name2", Employees = new List<Employee> { new Employee { Id = Guid.NewGuid(), ShopId = secondShopId, EmailAddress = "adress", FirstName = "first", LastName = "last", Position = "post", Salary = 2010 } } };

        var contextOptions = new DbContextOptionsBuilder<CwContext>()
            .UseInMemoryDatabase(databaseName: "GetShopListAsync_ShouldReturnShopListWithAverageProductivity")
            .Options;
        var context = new CwContext(contextOptions);
        context.Shops.AddRange(shop1, shop2);
        await context.SaveChangesAsync();

        var employeesServiceMock = new Mock<IEmployeesService>();
        employeesServiceMock.Setup(s => s.GetProductivity(It.IsAny<Employee>())).Returns(5.0);

        var shopsService = new ShopsService(context, employeesServiceMock.Object);

        // Act
        var result = await shopsService.GetShopListAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }
}