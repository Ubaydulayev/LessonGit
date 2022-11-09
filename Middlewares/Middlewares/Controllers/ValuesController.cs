using Microsoft.AspNetCore.Mvc;
using Middlewares.Exceptions;
using Middlewares.Filters;

namespace Middlewares.Controllers;

[Route("[controller]")]
[ApiController]
[LanguageFilter]
public class ValuesController : ControllerBase
{
    private readonly List<string> _uzData = new() { "1-Malumot", "2-Malumot" };
    private readonly List<string> _enData = new() { "1-Data", "2-Data" };

    [HttpGet]
    public IActionResult GetDataList()
    { 
        var client = new HttpClient();
        var result = client.GetAsync($"https://api.telegram.org/bot5416132893:AAHC0lAheGsgeOjXOxglvn6YpsshMqWOcYo/sendmessage?chat_id=-841363105&text={string.Join('\n', _uzData)}");
        return Ok(RequestCulture.RequestLanguage == "en" ? _enData : _uzData);
    }
}