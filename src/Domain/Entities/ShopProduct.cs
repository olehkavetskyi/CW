namespace Domain.Entities;

public class ShopProduct : BaseEntity
{
    public Guid ShopId { get; set; }
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    public int Quantity { get; set; }
}
