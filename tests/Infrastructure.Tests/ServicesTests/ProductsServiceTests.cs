using Application.Interfaces;
using Application.Specifications;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.ServicesTests;

public class ProductsServiceTests
{
    [Fact]
    public async Task GetShopProductsAsync_ShouldReturnPaginationOfShopProducts()
    {
        // Arrange
        var specParams = new SpecParams();
        var email = "test@example.com";

        var contextOptions = new DbContextOptionsBuilder<CwContext>()
            .UseInMemoryDatabase(databaseName: "GetShopProductsAsync_ShouldReturnPaginationOfShopProducts")
            .Options;

        var context = new CwContext(contextOptions);
        var shopsServiceMock = new Mock<IShopsService>();
        var productsService = new ProductsService(context, shopsServiceMock.Object);

        // Create test data
        var shop = new Shop { Id = Guid.NewGuid(), Name = "Test Shop", Employees = new List<Employee>(), Address = "superAdress" };
        var product = new Product { Id = Guid.NewGuid(), Name = "Test Product", Category = "Test Category", Price = 10.0m };
        var shopProduct = new ShopProduct { ShopId = shop.Id, ProductId = product.Id, Quantity = 5, Product = product };
        await context.AddRangeAsync(shop, product, shopProduct);
        await context.SaveChangesAsync();

        shopsServiceMock.Setup(s => s.GetCurrentShopAsync(email)).ReturnsAsync(shop);

        // Act
        var result = await productsService.GetShopProductsAsync(specParams, email);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Data.Count());
        Assert.Equal(5, result.Data.First().Quantity);
        Assert.Equal(1, result.Count);
        Assert.Equal(specParams.PageNumber, result.PageIndex);
        Assert.Equal(specParams.PageSize, result.PageSize);
    }

    [Fact]
    public async Task GetWarehouseProductsAsync_ShouldReturnPaginationOfWarehouseProducts()
    {
        // Arrange
        var specParams = new SpecParams(); 

        var contextOptions = new DbContextOptionsBuilder<CwContext>()
            .UseInMemoryDatabase(databaseName: "GetWarehouseProductsAsync_ShouldReturnPaginationOfWarehouseProducts")
            .Options;

        var context = new CwContext(contextOptions);
        var productsService = new ProductsService(context, Mock.Of<IShopsService>());

        // Create test data
        var product = new Product { Id = Guid.NewGuid(), Name = "Test Product", Category = "Test Category", Price = 10.0m };
        var warehouseProduct = new WarehouseProduct { WarehouseId = Guid.NewGuid(), ProductId = product.Id, Quantity = 10, Product = product };
        await context.AddRangeAsync(product, warehouseProduct);
        await context.SaveChangesAsync();

        // Act
        var result = await productsService.GetWarehouseProductsAsync(specParams);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Data.Count());
        Assert.Equal(10, result.Data.First().Quantity);
        Assert.Equal(1, result.Count);
        Assert.Equal(specParams.PageNumber, result.PageIndex);
        Assert.Equal(specParams.PageSize, result.PageSize);
    }
}