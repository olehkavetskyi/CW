using Domain.Entities;
using Infrastructure.Data.Dtos;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Data;

public class CwContextSeed
{
    public static async Task InitializeAsync(
        CwContext context, 
        ILoggerFactory loggerFactory, 
        UserManager<AppUser> userManager, 
        RoleManager<AppRole> roleManager)
    {
        try
        {
            await SeedUsersAsync(userManager, roleManager);
            await SeedShopsAsync(context);
            await SeedEmployeesAsync(context);
            await SeedCustomersAsync(context);
            await SeedProductsAsync(context);
            await SeedEmployeeOrdersAsync(context);
            await SeedCustomerOrdersAsync(context);
            await SeedWarehousesAsync(context);
            await SeedWarehouseProductsAsync(context);
            await SeedShopProductsAsync(context);
        }
        catch (Exception ex) 
        {
            var logger = loggerFactory.CreateLogger<CwContextSeed>();
            logger.LogError(ex.Message);
        }

    }

    public static async Task SeedUsersAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        if (userManager.Users.Any())
        {
            return;
        }

        var roles = new List<string> { "Admin", "Cashier", "Manager" };
        foreach (var roleName in roles)
        {
            try
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new AppRole { Name = roleName };
                    try
                    {
                        await roleManager.CreateAsync(role);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred while creating the user: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while creating the user: " + ex.Message);
            }
        }

        var jsonData = File.ReadAllText(@"../Infrastructure/Data/SeedData/users.json");
        var users = JsonConvert.DeserializeObject<UserSeedDto[]>(jsonData);

        foreach (var userSeedData in users!)
        {
            var existingUser = await userManager.FindByEmailAsync(userSeedData.Email);

            if (existingUser == null)
            {
                var user = new AppUser
                {
                    UserName = userSeedData.Email,
                    Email = userSeedData.Email,
                    Role = userSeedData.Role
                };
                IdentityResult result = null!;
                try
                {
                    result = await userManager.CreateAsync(user, userSeedData.Password);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while creating the user: " + ex.Message);
                }

                if (result.Succeeded)
                {
                    if (!await roleManager.RoleExistsAsync(userSeedData.Role.ToString()))
                    {
                        await roleManager.CreateAsync(new AppRole { Name = userSeedData.Role.ToString() });
                    }

                    await userManager.AddToRoleAsync(user, userSeedData.Role.ToString());
                }
            }
        }
    }

    private async static Task SeedEmployeesAsync(CwContext context)
    {
        if (!(await context.Employees.AnyAsync()))
        {
            var employeesData = File.ReadAllText(@"../Infrastructure/Data/SeedData/employees.json");

            var employees = JsonConvert.DeserializeObject<Employee[]>(employeesData);

            await context.Employees.AddRangeAsync(employees!);

            await context.SaveChangesAsync();
        }
    }

    private async static Task SeedWarehousesAsync(CwContext context)
    {
        if (!(await context.Warehouses.AnyAsync()))
        {
            var warehousesData = File.ReadAllText(@"../Infrastructure/Data/SeedData/warehouses.json");

            var warehouses = JsonConvert.DeserializeObject<Warehouse[]>(warehousesData);

            await context.Warehouses.AddRangeAsync(warehouses!);

            await context.SaveChangesAsync();
        }
    }

    private async static Task SeedCustomersAsync(CwContext context)
    {
        if (!(await context.Customers.AnyAsync()))
        {
            var customersData = File.ReadAllText(@"../Infrastructure/Data/SeedData/customers.json");

            var customers = JsonConvert.DeserializeObject<Customer[]>(customersData);

            await context.Customers.AddRangeAsync(customers!);

            await context.SaveChangesAsync();
        }
    }

    private async static Task SeedProductsAsync(CwContext context)
    {
        if (!(await context.Products.AnyAsync()))
        {
            var productsData = File.ReadAllText(@"../Infrastructure/Data/SeedData/products.json");

            var products = JsonConvert.DeserializeObject<Product[]>(productsData);

            await context.Products.AddRangeAsync(products!);

            await context.SaveChangesAsync();
        }
    }

    private async static Task SeedEmployeeOrdersAsync(CwContext context)
    {
        if (!(await context.EmployeeOrders.AnyAsync()))
        {
            var employeeOrdersData = File.ReadAllText(@"../Infrastructure/Data/SeedData/employeeOrders.json");

            var employeeOrders = JsonConvert.DeserializeObject<EmployeeOrder[]>(employeeOrdersData);

            await context.EmployeeOrders.AddRangeAsync(employeeOrders!);

            await context.SaveChangesAsync();
        }
    }

    private async static Task SeedCustomerOrdersAsync(CwContext context)
    {
        if (!(await context.CustomerOrders.AnyAsync()))
        {
            var customerOrdersData = File.ReadAllText(@"../Infrastructure/Data/SeedData/customerOrders.json");

            var customerOrders = JsonConvert.DeserializeObject<CustomerOrder[]>(customerOrdersData);

            await context.CustomerOrders.AddRangeAsync(customerOrders!);

            await context.SaveChangesAsync();
        }
    }

    private async static Task SeedShopsAsync(CwContext context)
    {
        if (!(await context.Shops.AnyAsync()))
        {
            var shopsData = File.ReadAllText(@"../Infrastructure/Data/SeedData/shops.json");

            var shops = JsonConvert.DeserializeObject<Shop[]>(shopsData);

            await context.Shops.AddRangeAsync(shops!);

            await context.SaveChangesAsync();
        }
    }

    private async static Task SeedWarehouseProductsAsync(CwContext context)
    {
        if (!(await context.WarehouseProducts.AnyAsync()))
        {
            var warehouseProductData = File.ReadAllText(@"../Infrastructure/Data/SeedData/warehouseProducts.json");

            var warehouseProducts = JsonConvert.DeserializeObject<WarehouseProduct[]>(warehouseProductData);

            await context.WarehouseProducts.AddRangeAsync(warehouseProducts!);

            await context.SaveChangesAsync();
        }
    }

    private async static Task SeedShopProductsAsync(CwContext context)
    {
        if (!(await context.ShopProducts.AnyAsync()))
        {
            var shopProductsData = File.ReadAllText(@"../Infrastructure/Data/SeedData/shopProducts.json");

            var shopProducts = JsonConvert.DeserializeObject<ShopProduct[]>(shopProductsData);

            await context.ShopProducts.AddRangeAsync(shopProducts!);

            await context.SaveChangesAsync();
        }
    }
}