namespace Domain.Entities;

public class CustomerOrder : BaseEntity
{
    public Guid? CustomerId { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid ShopId { get; set; }
    public List<CustomerOrderProduct> Products { get; set; } = [];
    public DateTime Date { get; set; }
}
