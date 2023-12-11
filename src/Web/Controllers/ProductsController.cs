using Application.Helpers;
using Application.Interfaces;
using Application.Specifications;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Authorize]
public class ProductsController : BaseController
{
    private readonly IProductsService _productsService;

    public ProductsController(IProductsService productsService)
    {
        _productsService = productsService;
    }

    [HttpGet("warehouse")]
    public async Task<ActionResult<Pagination<WarehouseProduct>>> GetWarehouseProductsAsync([FromQuery]SpecParams spec)
    {
        var result = await _productsService.GetWarehouseProductsAsync(spec);

        return Ok(result);
    }

    [HttpGet("shop")]
    public async Task<ActionResult<Pagination<ShopProduct>>> GetShopProductsAsync([FromQuery] SpecParams spec)
    {
        var result = await _productsService.GetShopProductsAsync(spec, User.Claims.First().Value);

        return Ok(result);
    }
}
