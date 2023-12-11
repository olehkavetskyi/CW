namespace Domain.Entities;

public class EmployeeOrder : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public DateTime Date {  get; set; }
    public List<EmployeeOrderProduct> Products { get; set; } = [];
}
