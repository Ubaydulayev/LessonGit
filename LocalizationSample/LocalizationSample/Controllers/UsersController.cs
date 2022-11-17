using LocalizationSample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LocalizationSample.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost]
    public IActionResult PostUser(string culture, [FromBody] User user)
    {
        if (ModelState.IsValid)
        {
            return Ok(user);
        }

        return Ok(ModelState);
    }
}