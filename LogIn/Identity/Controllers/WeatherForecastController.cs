using Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _logger = logger;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [Authorize]
    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpPost]
    public async Task<IActionResult> Login()
    {
        var user = new User()
        {
            UserName = "User Name",
            ChatId = 1
        };

        await _userManager.CreateAsync(user, "1241243@asfD.cs");
        await _signInManager.SignInAsync(user, isPersistent: true);

        return Ok();
    }
}

