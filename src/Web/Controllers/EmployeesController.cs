using Application.Dtos;
using Application.Extensions;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Authorize]
public class EmployeesController : BaseController
{
    private readonly IEmployeesService _employeesService;

    public EmployeesController(IEmployeesService employeesService)
    {
        _employeesService = employeesService;
    }

    [HttpGet("current")]
    public async Task<ActionResult<EmployeeDto>> GetCurrentEmployeeAsync()
    {
        var employee = await _employeesService.GetEmployeeAsync(User.Claims.First().Value);

        if (employee == null)
        {
            return NotFound();
        }

        var productivity = _employeesService.GetProductivity(employee);

        return Ok(employee.ToDto(productivity));
    }
}
