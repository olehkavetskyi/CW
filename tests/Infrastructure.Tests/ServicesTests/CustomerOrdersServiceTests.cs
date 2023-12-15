using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.ServicesTests;

public class CustomerOrdersServiceTests
{
    [Fact]
    public async Task AddCustomerOrderAsync_ShouldAddOrderAndAdjustQuantities()
    {
        // Arrange
        var email = "test@example.com";
        var contextOptions = new DbContextOptionsBuilder<CwContext>()
            .UseInMemoryDatabase(databaseName: "AddCustomerOrderAsync_ShouldAddOrderAndAdjustQuantities")
            .Options;

        var context = new CwContext(contextOptions);
        var employeesServiceMock = new Mock<IEmployeesService>();
        employeesServiceMock.Setup(e => e.GetEmployeeAsync(email))
            .ReturnsAsync(new Employee { Id = Guid.NewGuid(), ShopId = Guid.NewGuid() });

        var customerOrdersService = new CustomerOrdersService(context, null, employeesServiceMock.Object);

        // Create test data
        var product1 = new Product {Id = Guid.NewGuid(), Name = "y23", Category = "bomb" };
        var product2 = new Product { Id = Guid.NewGuid(), Name = "y23", Category = "bomb"  };
        var shopProduct1 = new ShopProduct { ProductId = product1.Id, Quantity = 10 };
        var shopProduct2 = new ShopProduct { ProductId = product2.Id, Quantity = 15 };
        var customerOrderProducts = new List<CustomerOrderProduct>
            {
                new CustomerOrderProduct { ProductId = product1.Id, Quantity = 2 },
                new CustomerOrderProduct { ProductId = product2.Id, Quantity = 3 },
            };

        context.Products.AddRange(product1, product2);
        context.ShopProducts.AddRange(shopProduct1, shopProduct2);
        context.SaveChanges();

        var customerOrder = new CustomerOrderDto()
        {
            Products = customerOrderProducts,
            CustomerEmail = "amil@test.com"
        };

        // Act
        await customerOrdersService.AddCustomerOrderAsync(customerOrder, email);

        // Assert
        var addedOrder = await context.CustomerOrders.FirstOrDefaultAsync();
        Assert.NotNull(addedOrder);
        Assert.Equal(2, addedOrder.Products.Count);

        var updatedShopProduct1 = await context.ShopProducts
            .FirstOrDefaultAsync(sp => sp.ProductId == product1.Id);
        var updatedShopProduct2 = await context.ShopProducts
            .FirstOrDefaultAsync(sp => sp.ProductId == product2.Id);

        Assert.NotNull(updatedShopProduct1);
        Assert.NotNull(updatedShopProduct2);
        Assert.Equal(8, updatedShopProduct1.Quantity); 
        Assert.Equal(12, updatedShopProduct2.Quantity); 
    }
}