using Application.Dtos;
using Domain.Entities;

namespace Application.Extensions;

public static class ShopExtensions
{
    public static ShopDto ToDto(this Shop shop, double efficiency)
    {
        return new ShopDto
        {
            Id = shop.Id,
            Name = shop.Name,
            Address = shop.Address,
            AverageEfficiency = efficiency,
            EmployeesCount = shop.Employees.Count,
        };
    }
}
