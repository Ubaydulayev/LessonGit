using System.ComponentModel.DataAnnotations;
using LocalizationSample.Validators;

namespace LocalizationSample.Models;

public class User
{
    [UserRequired(ErrorMessage = "User.Username")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "User.Password")]
    public string? Password { get; set; }
}