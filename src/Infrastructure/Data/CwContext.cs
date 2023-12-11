using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class CwContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Shop> Shops { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeOrder> EmployeeOrders { get; set; }
    public DbSet<CustomerOrder> CustomerOrders { get; set; }
    public DbSet<WarehouseProduct> WarehouseProducts { get; set; }
    public DbSet<ShopProduct> ShopProducts { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }


    public CwContext(DbContextOptions<CwContext> options) : base(options)
    {
    }

    public CwContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
