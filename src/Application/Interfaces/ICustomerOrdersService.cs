using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces;

public interface ICustomerOrdersService
{
    Task AddCustomerOrderAsync(CustomerOrderDto customerOrderDto, string email);
    Task<List<CustomerOrder>> GetCurrentCustomerOrdersAsync(string email);
}
