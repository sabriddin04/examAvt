using System.Data.Common;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : Base
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string? Role { get; set; }
}
