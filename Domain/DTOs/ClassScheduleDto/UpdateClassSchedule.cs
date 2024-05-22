namespace Domain.DTOs.ClassSchduleDto;

public class UpdateClassSchedule
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public int Duration { get; set; }
    public string? Location { get; set; }
    public int WorkoutId { get; set; } 
}
