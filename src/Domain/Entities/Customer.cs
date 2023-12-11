namespace Domain.Entities;

public class Customer : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Email { get; set; } 
    public string? PhoneNumber { get; set; } 
}
