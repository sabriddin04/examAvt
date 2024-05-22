namespace Domain.DTOs.UserDto;

public class UpdateUser
{
    public int Id { get; set; }
    public  string Name { get; set; } = null!; 
    public  string Email { get; set; } = null!; 
    public  string Password { get; set; } = null!;
}
