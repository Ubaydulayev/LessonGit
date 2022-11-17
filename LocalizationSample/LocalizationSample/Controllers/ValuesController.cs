using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LocalizationSample.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.SwaggerGen;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LocalizationSample.Controllers;
[Route("api/[controller]")]
public class ValuesController : Controller
{
    private readonly ValuesService _valuesService;    
    private readonly Dictionary<string, string> _uzValues = new Dictionary<string, string>()
    {
        { "Value1", "UzMalumot" },
        { "Value2", "UzValue" }
    };

    private readonly Dictionary<string, string> _enValues = new Dictionary<string, string>()
    {
        { "Value1", "Daata" },
        { "Value2", "Value" }
    };

    private readonly Dictionary<string, string> _ruValues = new Dictionary<string, string>()
    {
        { "Value1", "Данные" },
        { "Value2", "Пример" }
    };

    private readonly IStringLocalizer<ValuesController> _stringLocalizer;

    public ValuesController(IStringLocalizer<ValuesController> stringLocalizer, ValuesService valuesService)
    {
        _stringLocalizer = stringLocalizer;
        _valuesService = valuesService;

    }

    // GET: api/values
    [HttpGet("from-localizer")]
    public IActionResult Get(string culture = "uz")
    {
        SetCulture(culture);
            
        return Ok(_stringLocalizer["Data"].Value);
    }
    [HttpGet("from-service")]
    public IActionResult GetFromService()
    {
        //SetCulture(culture); replaced with middleware
        HttpContext.Response.Cookies.Append(".AspNetCore.Culture", "en");
        return Ok(_valuesService.Value());
    }

    private void SetCulture(string culture)
    {
        if (culture == "uz")
        {
            CultureInfo.CurrentUICulture = new CultureInfo("Uz");
        }
        else if (culture == "en")
        {
            CultureInfo.CurrentUICulture = new CultureInfo("En");
        }
        else if (culture == "ru")
        {
            CultureInfo.CurrentUICulture = new CultureInfo("Ru");
        }
    }
}

    