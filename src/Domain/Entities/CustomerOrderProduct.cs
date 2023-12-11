namespace Domain.Entities;

public class CustomerOrderProduct : BaseEntity
{
    public Guid ShopId { get; set; }
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    public int Quantity { get; set; }
}
