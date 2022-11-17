
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Classrom.Api.Entities;
using Classrom.Api.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Classrom.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost("signUp")]
    public async Task<IActionResult> SignUp(SignUpUserDto signUp)
    {
        if (!ModelState.IsValid) return BadRequest();

        if (signUp.Password != signUp.ConfirmPassword) return BadRequest();

        if (!await _userManager.Users.AnyAsync(user => user.UserName == signUp.UserName)) return NotFound();

        var member = signUp.Adapt<User>();

        await _userManager.CreateAsync(member, signUp.Password);

        await _signInManager.SignInAsync(member, isPersistent: true);

        return Ok();
    }

    [HttpPost("signIn")]
    public async Task<IActionResult> SignIn(SignInUserDto signIn)
    {
        if (!ModelState.IsValid) return BadRequest();

        if (!await _userManager.Users.AnyAsync(user => user.UserName == signIn.UserName)) return NotFound();

        var result = await _signInManager.PasswordSignInAsync(signIn.UserName, signIn.Password, isPersistent: true, false);

        if (result.Succeeded) return BadRequest();

        return Ok();
    }

    [HttpGet("{UserName}")]
    [Authorize]
    public async Task<IActionResult> Profile(string UserName) 
    {
        var user = await _userManager.GetUserAsync(User);

        if (user.UserName != UserName) return NotFound();

        var userDto = user.Adapt<UserDto>();

        return Ok(userDto);
    }
}

