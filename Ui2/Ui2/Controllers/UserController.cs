using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ui2.Context;
using Ui2.Models;
using Ui2.Service;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ui2.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UserController : ControllerBase
{
    private readonly UserStore _store;
    private readonly UserDbContext _context;

    public UserController (UserStore store, UserDbContext context)
    {
        _store = store;
        _context = context;
    }
    [HttpGet]
    public IActionResult GetMe(string key)
    {
        if (!HttpContext.Request.Headers.ContainsKey("Key"))
            return Unauthorized();

        key = HttpContext.Request.Headers["Key"];

        if (!_store.Users.ContainsKey(key))
            return Unauthorized();
        
        return Ok(key);
    }
    [HttpGet("data")]
    public IActionResult GetData()
    {
        var key = HttpContext.Request.Headers["Key"];

        if (!HttpContext.Request.Headers.ContainsKey("Key"))
            return Unauthorized();
        
        if (!_store.Users.ContainsKey(key))
            return Unauthorized();

        return Ok(_store.Users.Values);
    }
    [HttpGet("public")]
    public IActionResult GetPublic()
    {
        return Ok("public");
    }
    [HttpPost]
    public IActionResult PostUser(User user)
    {
        var key = Guid.NewGuid().ToString();
        _store.Users.Add(key, user);
        return Ok(key);
    }
}


