
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PolicyAndCorsApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : Controller
{ 
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UsersController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult GetUser()
    {
        var user = new IdentityUser();

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp()
    {
        if (!await _roleManager.RoleExistsAsync("admin"))
        {
            var role = new IdentityRole("admin");
            await _roleManager.CreateAsync(role);
        }

        if(!await _roleManager.RoleExistsAsync("teacher"))
        {
            var role = new IdentityRole("teacher");
            await _roleManager.CreateAsync(role);
        }

        var user = new IdentityUser("User2");

        await _userManager.CreateAsync(user, "123@Asd.com");
        await _userManager.AddClaimAsync(user, new Claim("IsActive", "true"));

        await _userManager.AddToRoleAsync(user, "admin");
        await _userManager.AddToRoleAsync(user, "teacher");

        await _signInManager.SignInAsync(user, isPersistent: true);

        return Ok(user);
    }

    [HttpGet("denied")]
    public IActionResult Denied() => Unauthorized("Unauthorized");
}