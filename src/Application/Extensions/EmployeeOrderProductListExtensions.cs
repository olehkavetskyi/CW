using Domain.Entities;

namespace Application.Extensions;

public static class EmployeeOrderProductListExtensions
{
    public static EmployeeOrder ToEmployeeOrder(this List<EmployeeOrderProduct> employeeOrderProducts, Guid employeeId)
    {
        return new EmployeeOrder
        {
            Date = DateTime.UtcNow,
            Products = employeeOrderProducts,
            EmployeeId = employeeId
        };
    }
}


