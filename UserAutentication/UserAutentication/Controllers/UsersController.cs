using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using UserAutentication.Service;
using System.Threading.Tasks;
using UserAutentication.Filters;
using System.Security.Claims;
using UserAutentication.Models;
using Microsoft.AspNetCore.Mvc;
using UserAutentication.Context;


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
    //[TypeFilter(typeof(AuthFilterAttribute))]
    public IActionResult GetMe()
    {
        var user = User;
        return Ok();
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
        return Ok("data");
    }
    [HttpGet("public")]
    public IActionResult GetPublicData()
    {
        return Ok("Publishes");
    }
}
