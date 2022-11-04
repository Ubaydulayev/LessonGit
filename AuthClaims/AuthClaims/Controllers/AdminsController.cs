using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthClaims.Controllers;
[Route("Admins")]
public class AdminsController : ControllerBase
{
    [Authorize]
    [HttpGet]
    public IActionResult GetAdmin()
    {
        return Ok("User list");
    }
}