using Domain.Entities;

namespace Application.Interfaces;

public interface IEmployeeOrdersService
{
    Task AddEmployeeOrderAsync(List<EmployeeOrderProduct> employeeOrderProducts, string email);
    Task<List<EmployeeOrder>> GetCurrentEmployeeOrdersAsync(string email);
}
