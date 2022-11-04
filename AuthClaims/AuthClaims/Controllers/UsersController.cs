using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using AuthClaims.Context;
using AuthClaims.Filters;
using AuthClaims.Models;
using AuthClaims.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using static AuthClaims.Service.UserStore;

namespace AuthClaims.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserStore _store;
    private readonly UserDbContext _context;
    private readonly string _filePath;

    public UsersController(UserStore store, UserDbContext context, IOptions<JsonData> options)
    {
        _store = store;
        _context = context;
        _filePath = options.Value.Path ?? "file.json";
    }

    [HttpGet]
    [Role("user")]
    public IActionResult GetMe()
    {
        Claim? name = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

        return Ok("User Name: " + name?.Value);
    }

    [HttpGet("admin")]
    [Role("admin")]
    public IActionResult GetAdminMe()
    {
        Claim? name = User.Claims.FirstOrDefault(d => d.Type == ClaimTypes.Name);
        Claim? Phone = User.Claims.FirstOrDefault(d => d.Type == ClaimTypes.MobilePhone);
        Claim? Email = User.Claims.FirstOrDefault(d => d.Type == ClaimTypes.Email);
        return Ok("User Name: " + name?.Value + ", Mobile Phone: " + Phone?.Value + ", Email: " + Email?.Value);
    }

    [HttpGet("cal")]
    public IActionResult Calculate(int a, int n)
    { 
        bool t = true;
        int count = 0;
        while(t)
        {
            for (int i = a; i < n; i++) {
                if (n % i == 0)
                {
                    count++;
                }
                else
                {
                    i -= 1;
                    n++;
                }
            }
            if (count == n)
                t = false;
        }
        
        return Ok(count);
    }

    [HttpPost]
    public IActionResult Post(User user)
    {
        var key = Guid.NewGuid().ToString();
        _store.Users.Add(key, user);

        var users = ReadUsers();
        users ??= new List<User>();

        users.Add(user);

        SaveUser(users);

        return Ok(key);
    }

    private void SaveUser(List<User> users)
    {
        var json = JsonConvert.SerializeObject(users);
        System.IO.File.WriteAllText(_filePath, json);
    }

    [HttpGet("{id}")]
    public IActionResult GetJsonId(int id)
    {
        var user = ReadUsers()?.FirstOrDefault(u => u.Id == id);
        if (user is null) return NotFound();

        return Ok(user);
    }

    [HttpGet("allUsers")]
    [Role("allUsers")]
    public IActionResult GetAllUsers()
    { return Ok(ReadUsers()); }
    

    private List<User>? ReadUsers()
    {
        if (!System.IO.File.Exists(_filePath)) return null;
        var json = System.IO.File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<User>>(json);
    }
}
