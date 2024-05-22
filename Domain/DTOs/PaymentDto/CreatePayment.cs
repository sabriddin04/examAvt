namespace Domain.DTOs.PaymentDto;

public class CreatePayment
{
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string? Status { get; set; }
    public int UserId { get; set; }
}
