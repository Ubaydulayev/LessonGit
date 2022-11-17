using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PolicyAndCorsApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly List<string> Summaries = new()
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    [Authorize]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Count)]
        })
        .ToArray();
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public IActionResult AddWeather(string summaries)
    {
        Summaries.Add(summaries);
        return Ok("Added, Count: " + Summaries.Count);
    }

    [HttpDelete]
    [Authorize(Roles = "admin, teacher")]
    public IActionResult RemoveWeather(int index)
    {
        Summaries.RemoveAt(index);
        return Ok("Removed, Count: "+ Summaries.Count);
    }

    [HttpGet("{index}")]
    [Authorize(Policy = "GetUserByIdPolicy")]
    [Authorize(Roles = "admin, teacher")]
    [Authorize(Policy = "GetWeatherPolicy")]
    public IActionResult GetWeatherById(int index)
    {
        var principal = User;
        return Ok(Summaries[index]);
    }
}

