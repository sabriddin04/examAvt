namespace Domain.DTOs.UserDto;

public class GetUser
{
    public int Id { get; set; }
    public required string Name { get; set; } 
    public required string Email { get; set; } 
    public required string Password { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
