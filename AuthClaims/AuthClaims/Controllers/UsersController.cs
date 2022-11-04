
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthClaims.Context;
using AuthClaims.Filters;
using AuthClaims.Models;
using AuthClaims.Service;
using Microsoft.AspNetCore.Mvc;
using static AuthClaims.Service.UserStore;

namespace AuthClaims.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserStore _store;
    private readonly UserDbContext _context;

    public UsersController(UserStore store, UserDbContext context)
    {
        _store = store;
        _context = context;
    }

    [HttpGet("user")]
    [Role("user")]
    public IActionResult GetMe()
    {
        Claim? name = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
        return Ok("User Name" + name?.Value);
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

    [HttpPost]
    public IActionResult Post(User user)
    {
        var key = Guid.NewGuid().ToString();
        _store.Users.Add(key, user);
        return Ok(key);
    }   
}
