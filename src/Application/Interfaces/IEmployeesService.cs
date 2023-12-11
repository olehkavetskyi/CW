using Application.Dtos;
using Domain.Entities;
using System.Security.Claims;

namespace Application.Interfaces;

public interface IEmployeesService
{
    Task<Employee?> GetEmployeeAsync(string email);
    double GetProductivity(Employee employee);
}
