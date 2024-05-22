using Microsoft.AspNetCore.Http;

namespace Domain.DTOs.TrainerDto;

public class CreateTrainer
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Specialization { get; set; }
    public required IFormFile? PathPhoto { get; set; }
}
