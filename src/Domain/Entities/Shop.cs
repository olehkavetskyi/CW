namespace Domain.Entities;

public class Shop : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public List<Employee> Employees { get; set; } = [];
    public List<ShopProduct> Products { get; set; } = [];
}
