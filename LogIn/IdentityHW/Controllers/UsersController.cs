using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityHW.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly SignInManager<IdentityUser> _signManager;
    private readonly UserManager<IdentityUser> _userManager;

    public UsersController(ILogger<UsersController> logger, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _signManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation($"Userni Malumotlari");
        return Ok("Malumotlar");
    }
    [HttpPost("signUp")]
    public async ValueTask<IActionResult> SignUp(string name, string password)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
        {
            _logger.LogError("Sign Up failed");
            return BadRequest("SignUp failed");
        }
        var user = new IdentityUser()
        {
            UserName = name,
        };
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            return BadRequest("User unathorized");

        _logger.LogInformation("User succesfuly added");
        return Ok($"{name}, {password}");
    }
    [HttpPost("signIn")]
    public async ValueTask<IActionResult> SignIn(string name, string password)
    {
        if (string.IsNullOrEmpty(name))
        {
            _logger.LogError("SignIn failed");
            return BadRequest("SignIn failed");
        }
        var user = await _userManager.FindByNameAsync(name);
        if (user is null) { _logger.LogError("SignIn da hatolik bor");
            return NotFound("Bu isimliy user topilmadi");
        }
        var result = await _signManager.PasswordSignInAsync(user, password, true, false);
        _logger.LogInformation("User succesfuly tizimga kirdi");

        if (!result.Succeeded) return BadRequest("Sign In qilolmadi");
        _logger.LogInformation("Thank you grandMa");
        return Ok();
    }
    [Authorize]
    [HttpPost("LogOut")]
    public async ValueTask<IActionResult> LogOut()
    {
        _logger.LogInformation("User logged out");
        await _signManager.SignOutAsync();
        return Ok();
    }

}