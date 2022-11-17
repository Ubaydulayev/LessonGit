using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapper.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mapper.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : Controller
{
    private User UserObject = new User()
    {
        Id = 1,
        Name = "Sad",
        Email = "123@gma.co",
        Password = "1234"
    };
    private Question Question = new Question()
    {
        Title = "1-savol",
        Choice = "1, 2, 3"
    };

    [HttpGet]
    public IActionResult GetUser()
    {
        var userDto = UserObject.ToDto();
        return Ok(userDto);
    }
    [HttpGet("question")]
    public IActionResult GetQuestion()
    {
        var questionDto = Question.ToQuestionDto();

        return Ok(questionDto);
    }
}
