using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using UserAutentication.Context;
using UserAutentication.Filters;
using UserAutentication.Models;
using UserAutentication.Service;

namespace UserAutentication.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UsersStore _store;
    private readonly UserDbContext _context;

    public UsersController(UsersStore store, UserDbContext context)
    {
        _store = store;
        _context = context;
    }

    [HttpGet]
    [TypeFilter(typeof(AuthFilterAttribute))]
    public IActionResult GetMe()
    {
        var claims = new List<Claim>() { new Claim(ClaimTypes.Name, "User1"),
        new Claim(ClaimTypes.HomePhone, "90854"), new Claim("Password", "1234")};

        var claim = new ClaimsIdentity(claims);
        
        var user = new ClaimsPrincipal(claim);
        return Ok(claim.Name);
    }
    [HttpPost]
    public IActionResult UserRegister(User user)
    {
        var key = Guid.NewGuid().ToString("N");
        _store.Users.Add(key, user);

        return Ok(key);
    }
    [HttpGet("data")]
    [TypeFilter(typeof(AuthFilterAttribute))]
    public IActionResult GetData()
    {
        return Ok(_store.Users.Values);
    }
    [HttpGet("public")]
    public IActionResult GetPublicData(int n, int k)
    {
        int count = 6, i;
        for(i = k; i < n; i++)
        {
            int digit = n % 10;
            if (digit == k)
                count++;
            n--;
        }    

        return Ok(count);
    }
}