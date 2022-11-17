using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Policy.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
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

    [HttpPost]
    public async Task<IActionResult> SignUp()
    {
        if (!await _roleManager.RoleExistsAsync("Admin"))
        {
            var role = new IdentityRole("Admin");
            await _roleManager.CreateAsync(role);
        }

        if (!await _roleManager.RoleExistsAsync("Manager"))
        {
            var role = new IdentityRole("Manager");
            await _roleManager.CreateAsync(role);
        }

        var user = new IdentityUser("User");

        await _userManager.CreateAsync(user, "1234");
        await _userManager.AddClaimAsync(user, new Claim("UserAge", "21"));
        await _userManager.AddClaimAsync(user, new Claim("IsActive", "true"));
        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Moderator"));

        await _userManager.AddToRoleAsync(user, "Admin");
        await _userManager.AddToRoleAsync(user, "Manager");

        await _signInManager.SignInAsync(user, isPersistent: true);

        return Ok(user);
    }

    [HttpGet("denied")]
    public IActionResult Denied() => Unauthorized("Unauthorized");
}