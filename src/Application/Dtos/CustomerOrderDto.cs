using Domain.Entities;

namespace Application.Dtos;

public class CustomerOrderDto
{
    public string? CustomerEmail { get; set; }
    public List<CustomerOrderProduct> Products { get; set; } = [];
}
