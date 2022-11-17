using LogIn.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LogIn.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly LogIn.Service.ILogger _logger;

    public UsersController()
    {
        _logger = new FileLogger();
    }
    // GET: api/values
    [HttpGet]
    public IActionResult GetUser()
    {
        _logger.Log($"{nameof(GetUser)} action chaqirildi.");
        try
        {
            int a = (int)((object)"sd");
            _logger.Log("Casted");
        }
        catch(InvalidCastException e)
        {
            _logger.Log($"Error: { e.Message}, Date: {DateTime.Now}");
        }
        return Ok("Data");
    }
}
