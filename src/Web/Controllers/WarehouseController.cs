using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers;

public class WarehouseController : BaseController
{
    private readonly IWarehouseService _warehouseService;

    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<Warehouse> GetWarehouseAsync()
    {
        var role = User.FindFirstValue(ClaimTypes.Role);

        return await _warehouseService.GetWarehouseAsync();
    }
}
