namespace Domain.Entities;

public class Employee : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Position { get; set; } = null!;
    public string EmailAddress { get; set; } = null!;
    public decimal Salary { get; set; }
    public Guid? ShopId { get; set; }
}
