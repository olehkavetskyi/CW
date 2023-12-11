using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Authorize]
public class ShopsController : BaseController
{
    private readonly IShopsService _shopsService;

    public ShopsController(IShopsService shopsService)
    {
        _shopsService = shopsService;
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public async Task<List<ShopDto>> GetShopListAsync()
    {
        var result = await _shopsService.GetShopListAsync();

        return result;
    }

    [HttpGet("current")]
    public async Task<ActionResult<Shop>> GetCurrentShopAsync()
    {
        var shop = await _shopsService.GetCurrentShopAsync(User.Claims.First().Value);

        if (shop == null)
        {
            return NotFound();
        }

        return Ok(shop);
    }

    [HttpGet("average-productivity")]
    public async Task<ActionResult<double>> GetAverageProductivity()
    {
        var currentShop = await _shopsService.GetCurrentShopAsync(User.Claims.First().Value);
        var averageProductivity = await _shopsService.GetAverageProductivityAsync(currentShop!.Id);

        return averageProductivity;
    }
}
