namespace Domain.DTOs.ClassSchduleDto;

public class CreateClassSchedule
{
    public DateTime DateTime { get; set; }
    public int Duration { get; set; }
    public string? Location { get; set; }
    public int WorkoutId { get; set; } 
}
