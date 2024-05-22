namespace Domain.Filters;

public class TrainerFilter : PaginationFilter
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}
