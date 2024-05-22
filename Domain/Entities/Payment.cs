namespace Domain.Entities;

public class Payment : Base
{
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string? Status { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}
