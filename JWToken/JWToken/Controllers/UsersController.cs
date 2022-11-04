using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWToken.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController : ControllerBase
{
    // GET: api/values
    [HttpGet]
    public IActionResult GetUsers()
    {
        //return Ok(User.FindFirst(ClaimTypes.Name)?Value;
        return Ok();
    }

    // POST api/values
    [HttpPost]
    [AllowAnonymous]
    public IActionResult Post(string userName)
    {
        return Ok();
    }
}
