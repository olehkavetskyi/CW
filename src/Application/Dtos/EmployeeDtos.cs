namespace Application.Dtos;

public class EmployeeDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Position { get; set; } = null!;
    public string EmailAddress { get; set; } = null!;
    public decimal Salary { get; set; }
    public double Productivity { get; set; }
}
