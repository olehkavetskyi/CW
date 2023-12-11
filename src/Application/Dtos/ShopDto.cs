namespace Application.Dtos;

public class ShopDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int EmployeesCount { get; set; }
    public string Address { get; set; } = null!;
    public double AverageEfficiency { get; set; }
}
