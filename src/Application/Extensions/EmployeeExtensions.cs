using Application.Dtos;
using Domain.Entities;

namespace Application.Extensions;

public static class EmployeeExtensions
{
    public static EmployeeDto ToDto(this Employee employee, double productivity)
    {
        return new EmployeeDto
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            EmailAddress = employee.EmailAddress,
            Position = employee.Position,
            Salary = employee.Salary,
            Productivity = productivity
        };
    }
}
