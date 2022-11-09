using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;
using JWToken.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWToken.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly string? _filePath;
    private readonly string _key;
    public UsersController(IOptions<JsonPathOption> options)
    {
        _filePath = options.Value.JsonPath ?? "file.json";
        _key = options.Value.Key!;
    }

    [HttpGet]
    public IActionResult GetUsers(string name)
    {
        var user = ReadUser()?.FirstOrDefault(u => u.Name == name && u.Role == "admin");
        if (user is null) return NotFound();
        return Ok(ReadUser());
    }
    [HttpGet("SignIn")]
    public IActionResult SignIn(string Phone)
    {
        var user = ReadUser()?.FirstOrDefault(u => u.Phone == Phone);
        if (user is null) return NotFound();
        return Ok(user);
    }
    private void SaveUser(List<User> users)
    {
        var json = JsonConvert.SerializeObject(users);
        System.IO.File.WriteAllText(_filePath!, json);
    }
    private List<User>? ReadUser()
    {
        if (!System.IO.File.Exists(_filePath)) return null;
        var json = System.IO.File.ReadAllText(_filePath!);
        return JsonConvert.DeserializeObject<List<User>>(json);
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult SignUp(User user)
    {
        var keyByte = System.Text.Encoding.UTF8.GetBytes(_key);
        var securityKey = new SigningCredentials(new SymmetricSecurityKey(keyByte), SecurityAlgorithms.HmacSha256);
        var security = new JwtSecurityToken(
            issuer: "Project", audience: "Room",
            new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Name!),
                new Claim(ClaimTypes.MobilePhone, user.Phone!),
                new Claim(ClaimTypes.Role, user.Role!)
            },
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: securityKey);
        var token = new JwtSecurityTokenHandler().WriteToken(security);
        var users = ReadUser();
        users ??= new List<User>();
        users.Add(user);

        SaveUser(users);

        return Ok(token);
    }
}
