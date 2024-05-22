using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.AuthDto;

public class RegisterDto
{
    public required string UserName { get; set; }
    [EmailAddress]
    public required string Email { get; set; }
    [DataType(DataType.Password)]
    public required string Password { get; set; }
    [DataType(DataType.Password)]
    [Compare("Password")]
    public required string ConfirmPassword { get; set; }
}