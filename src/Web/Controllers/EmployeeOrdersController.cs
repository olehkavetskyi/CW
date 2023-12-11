using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Authorize]
public class EmployeeOrdersController : BaseController
{
    private readonly IEmployeeOrdersService _employeeOrdersService;

    public EmployeeOrdersController(IEmployeeOrdersService employeeOrdersService)
    {
        _employeeOrdersService = employeeOrdersService;
    }

    [HttpGet("current")]
    public async Task<ActionResult<List<EmployeeOrder>>> GetCurrentEmployeeOrdersAsync()
    {
        var employeeOrders = await _employeeOrdersService.GetCurrentEmployeeOrdersAsync(User.Claims.First().Value);


        return Ok(employeeOrders);
    }

    [HttpPost("add")]
    public async Task<ActionResult> AddEmployeeOrderAsync(List<EmployeeOrderProduct> employeeOrderProducts)
    {
        await _employeeOrdersService.AddEmployeeOrderAsync(employeeOrderProducts, User.Claims.First().Value);

        return Ok();
    }
}
