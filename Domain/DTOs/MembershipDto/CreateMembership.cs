using Microsoft.AspNetCore.Http;

namespace Domain.DTOs.MembershipDto;

public class CreateMembership
{
    public required string Type { get; set; } 
    public decimal Price { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public required IFormFile? Photo { get; set; }
    public int UserId { get; set; } 
}