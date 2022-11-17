using LogIn.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LogIn.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    private readonly ILogger _logger;

    public ValuesController()
    {
        _logger = new MyFileLogger();
    }

    // GET: api/values
    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("GetValues called");
        _logger.LogError(new NotImplementedException(), "GetValues called with error");
        return Ok("Values");
    }

}
