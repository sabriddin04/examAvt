using Microsoft.AspNetCore.Http;

namespace Domain.DTOs.TrainerDto;

public class GetTrainer
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Specialization { get; set; }
    public string? PathPhoto { get; set; }
}
