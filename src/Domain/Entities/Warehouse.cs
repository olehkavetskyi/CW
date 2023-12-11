namespace Domain.Entities;

public class Warehouse : BaseEntity
{
    public ICollection<WarehouseProduct> WarehouseProducts { get; set; } = [];
}