namespace Application.Specifications;

public class SpecParams
{
    public int PageNumber { get; set; } = 1;
    public int? PageSize { get; set; }
    public string? FindElement { get; set; }
}
