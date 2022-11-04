using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

using SaveUsersToJsonWithOptions.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SaveUsersToJsonWithOptions.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly string _filePath;

    public UsersController(IOptions<JsonFileOption> options)
        => _filePath = options.Value.FilePath ?? "file.json";

    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok(ReadUsers());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var user = ReadUsers()?.FirstOrDefault(u => u.Id == id);

        if (user is null)
            return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public IActionResult Post(User user)
    {
        var users = ReadUsers();
        users ??= new List<User>();

        users.Add(user);

        SaveUser(users);

        return Ok();
    }

    private void SaveUser(List<User> users)
    {
        var jsonData = JsonConvert.SerializeObject(users);
        System.IO.File.WriteAllText(_filePath, jsonData);
    }

    private List<User>? ReadUsers()
    {
        if (!System.IO.File.Exists(_filePath))
            return null;
        var json = System.IO.File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<User>>(json);
    }
}
