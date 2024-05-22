using Microsoft.AspNetCore.Http;

namespace Domain.DTOs.MembershipDto;

public class GetMembership
{
    public int Id { get; set; }
    public string? Type { get; set; } 
    public decimal Price { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public IFormFile? Photo { get; set; }
    public int UserId { get; set; } 
}
