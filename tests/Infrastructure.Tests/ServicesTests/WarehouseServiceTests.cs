using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.ServicesTests;
public class WarehouseServiceTests
{
    [Fact]
    public async Task GetWarehouseAsync_ShouldReturnWarehouseWithProducts()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var product1 = new Product { Id = Guid.NewGuid(), Name = "Product1", Category = "R" };
        var product2 = new Product { Id = Guid.NewGuid(), Name = "Product2", Category = "T" };
        var warehouseProduct1 = new WarehouseProduct { Id = Guid.NewGuid(), Product = product1, Quantity = 10 };
        var warehouseProduct2 = new WarehouseProduct { Id = Guid.NewGuid(), Product = product2, Quantity = 20 };
        var warehouse = new Warehouse { Id = warehouseId, WarehouseProducts = new List<WarehouseProduct> { warehouseProduct1, warehouseProduct2 } };

        var contextOptions = new DbContextOptionsBuilder<CwContext>()
            .UseInMemoryDatabase(databaseName: "GetWarehouseAsync_ShouldReturnWarehouseWithProducts")
            .Options;
        var context = new CwContext(contextOptions);
        context.Warehouses.Add(warehouse);
        await context.SaveChangesAsync();

        var warehouseService = new WarehouseService(context);

        // Act
        var result = await warehouseService.GetWarehouseAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Warehouse>(result);
        Assert.Equal(warehouseId, result.Id);
        Assert.NotNull(result.WarehouseProducts);
        Assert.Equal(2, result.WarehouseProducts.Count);
        Assert.Contains(result.WarehouseProducts, wp => wp.Product.Name == "Product1" && wp.Quantity == 10);
        Assert.Contains(result.WarehouseProducts, wp => wp.Product.Name == "Product2" && wp.Quantity == 20);
    }

    [Fact]
    public async Task GetWarehouseAsync_ShouldReturnNullWhenWarehouseNotExists()
    {
        // Arrange
        var contextOptions = new DbContextOptionsBuilder<CwContext>()
            .UseInMemoryDatabase(databaseName: "GetWarehouseAsync_ShouldReturnNullWhenWarehouseNotExists")
            .Options;

        var context = new CwContext(contextOptions);
        var warehouseService = new WarehouseService(context);

        // Act
        var result = await warehouseService.GetWarehouseAsync();

        // Assert
        Assert.Null(result);
    }

}