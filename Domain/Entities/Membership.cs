using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class Membership : Base
{
    public string? Type { get; set; } 
    public decimal Price { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string PathPhoto  { get; set; }=null!;
    public int UserId { get; set; } 
    public User? User { get; set; }
}
