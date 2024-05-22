using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Base
{
    [Key]
    public int Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public  DateTimeOffset UpdatedAt { get; set; }
}
