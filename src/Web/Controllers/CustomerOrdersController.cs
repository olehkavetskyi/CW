using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Authorize]
public class CustomerOrdersController : BaseController
{
    private readonly ICustomerOrdersService _customerOrdersService;

    public CustomerOrdersController(ICustomerOrdersService customerOrdersService)
    {
        _customerOrdersService = customerOrdersService;
    }

    [HttpGet("current")]
    public async Task<ActionResult<CustomerOrder>> GetCurrentCustomerOrdersAsync()
    {
        var customerOrders = await _customerOrdersService.GetCurrentCustomerOrdersAsync(User.Claims.First().Value);

        return Ok(customerOrders);
    }

    [HttpPost("add")]
    public async Task<ActionResult> AddCustomerOrderAsync(CustomerOrderDto customerOrderDto)
    {
        await _customerOrdersService.AddCustomerOrderAsync(customerOrderDto, User.Claims.First().Value);

        return Ok();
    }
}
