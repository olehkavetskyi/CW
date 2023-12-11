using Application.Helpers;
using Application.Interfaces;
using Application.Specifications;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ProductsService : IProductsService
{
    private readonly CwContext _context;
    private readonly IShopsService _shopsService;

    public ProductsService(CwContext context, IShopsService shopsService)
    {
        _context = context;
        _shopsService = shopsService;
    }

    public async Task<Pagination<ShopProduct>> GetShopProductsAsync(SpecParams specParams, string email)
    {
        var spec = new GenericSpecification<ShopProduct>(specParams);

        var currentShop = await _shopsService.GetCurrentShopAsync(email);

        var productsQuery = SpecificationEvaluator<ShopProduct>
            .GetQuery(
                _context
                .Set<ShopProduct>()
                .AsQueryable()
                .Include(x => x.Product)
                .Where(
                    p => p.Product.Name.Contains(specParams.FindElement ?? "")
                    && p.ShopId == currentShop.Id), spec);
        try
        {
            var products = await productsQuery.ToListAsync();

            var totalItems = await _context
                .ShopProducts
                .Where(p => p.Product.Name.Contains(specParams.FindElement ?? "")
                && p.ShopId == currentShop.Id).CountAsync();


            if (!specParams.PageSize.HasValue)
            {
                specParams.PageSize = totalItems;
            }

            return new Pagination<ShopProduct>(specParams.PageNumber, (int)specParams.PageSize!, totalItems, products);
        }
        catch (Exception ex) { }

        return new Pagination<ShopProduct>(specParams.PageNumber, (int)specParams.PageSize!, 0, []);
    }

    public async Task<Pagination<WarehouseProduct>> GetWarehouseProductsAsync(SpecParams specParams)
    {
        var spec = new GenericSpecification<WarehouseProduct>(specParams);

        var productsQuery = SpecificationEvaluator<WarehouseProduct>
            .GetQuery(
                _context
                .Set<WarehouseProduct>()
                .AsQueryable()
                .Include(x => x.Product)
                .Where(
                    p => p.Product.Name.Contains(specParams.FindElement ?? "")), spec);

        try
        {
            var products = await productsQuery.ToListAsync();

            var totalItems = await _context
                .WarehouseProducts
                .Where(p => p.Product.Name.Contains(specParams.FindElement ?? "")).CountAsync();


            if (!specParams.PageSize.HasValue)
            {
                specParams.PageSize = totalItems;
            }

            return new Pagination<WarehouseProduct>(specParams.PageNumber, (int)specParams.PageSize!, totalItems, products);
        }
        catch (Exception ex) { }

        return null;
    }
}
