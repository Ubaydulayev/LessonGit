using System;
using Microsoft.EntityFrameworkCore;

namespace UserAutentication.Models;
//[Keyless]
public class User
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
}