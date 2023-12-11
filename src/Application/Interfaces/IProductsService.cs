using Application.Helpers;
using Application.Specifications;
using Domain.Entities;

namespace Application.Interfaces;

public interface IProductsService
{
    Task<Pagination<WarehouseProduct>> GetWarehouseProductsAsync(SpecParams spec);
    Task<Pagination<ShopProduct>> GetShopProductsAsync(SpecParams spec, string email);
}
