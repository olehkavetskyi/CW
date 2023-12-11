using Domain.Entities;

namespace Application.Extensions;

public static class CustomerOrderProductListExtensions
{
    public static CustomerOrder ToCustomerOrder(this List<CustomerOrderProduct> customerOrderProducts, Guid? customerId, Guid employeeId, Guid shopId)
    {
        return new CustomerOrder
        {
            Date = DateTime.UtcNow,
            Products = customerOrderProducts,
            CustomerId = customerId,
            EmployeeId = employeeId,
            ShopId = shopId,
        };
    }
}
