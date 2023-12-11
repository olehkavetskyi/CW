using Application.Dtos;
using Domain.Entities;
using System.Security.Claims;

namespace Application.Interfaces;

public interface IShopsService
{
    Task<double> GetAverageProductivityAsync(Guid id);
    Task<Shop?> GetCurrentShopAsync(string email);
    Task<List<ShopDto>> GetShopListAsync();
}
