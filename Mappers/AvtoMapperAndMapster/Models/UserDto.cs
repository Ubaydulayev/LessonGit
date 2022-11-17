using System;
namespace AvtoMapperAndMapster.Models;
public class UserDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? FirstName { get; set; }
    public int? IsActiveUser { get; set; }
    public decimal? Cost { get; set; }
}