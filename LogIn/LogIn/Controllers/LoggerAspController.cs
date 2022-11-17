using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LogIn.Controllers;

[Route("api/[controller]")]
public class LoggerAspController : ControllerBase
{
    private readonly ILogger _logger;

    public LoggerAspController(ILogger<LoggerAspController> logger)
    {
        _logger = logger;
    }

    // GET: api/values
    [HttpGet]
    public void Log()
    {
        _logger.LogInformation("Log method called In Logger Asp");
        _logger.LogError(new InvalidOperationException(), "Error called in aspController");
    }
}
