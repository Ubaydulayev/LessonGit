using Classroom.Web.Data;
using Classroom.Web.Dtos;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Classroom.Web.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet("/signup")]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost("/signup")]
    public async Task<IActionResult> SignUp([FromForm] UserCreateDto userCreateDto, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
            return View(userCreateDto);

        var user = await _userManager.FindByEmailAsync(userCreateDto.Email);
        if (user != null)
        {
            ModelState.AddModelError(nameof(userCreateDto.Email), "Email is registered.");
            return View(userCreateDto);
        }

        user = userCreateDto.Adapt<User>();
        var result = await _userManager.CreateAsync(user, userCreateDto.Password);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("Email", "Can not register.");
            return View(userCreateDto);
        }

        await _signInManager.SignInAsync(user, isPersistent: true);
        
        if (!string.IsNullOrEmpty(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction(nameof(Profile));
    }

    [Authorize]
    [HttpGet("/profile")]
    public async Task<IActionResult> Profile()
    {
        var user = await _userManager.GetUserAsync(User);
        
        if (user == null)
            return View();

        return View(user);
    }


    [HttpGet("/signin")]
    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost("/signin")]
    public async Task<IActionResult> SignIn(UserLoginDto userLoginDto)
    {
        var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
        if (user == null)
        {
            ModelState.AddModelError(nameof(userLoginDto.Email), "Email is not registered.");
            return View();
        }

        var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, true, false);

        if (result.Succeeded)
            return RedirectToAction(nameof(Profile));

        ModelState.AddModelError(nameof(userLoginDto.Email), "Email or password is incorrect");
        return View();
    }

    [AcceptVerbs("Get", "Post")]
    public async Task<IActionResult> IsEmailInUse(string email)
    {
        return await _userManager.FindByEmailAsync(email) != null ? 
            Json("Email is already in use.") : Json(true);
    }

    [AcceptVerbs("Get", "Post")]
    public async Task<IActionResult> IsEmailExists(string email)
    {
        return await _userManager.FindByEmailAsync(email) == null ?
            Json("Email is not registered.") : Json(true);
    }
}