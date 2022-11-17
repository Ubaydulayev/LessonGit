using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Classrom.Api.Context;
using Classrom.Api.Entities;
using Classrom.Api.Mappers;
using Classrom.Api.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Classrom.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ClassController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;

    public ClassController(AppDbContext context, SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _userManager = userManager;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetClass()
    {
        var classes = await _context.Classes.ToListAsync();
        List<ClassDto> classDto = classes.Select(c => c.ToDto()).ToList();
        return Ok(classDto);
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetClassById(Guid Id)
    {
        var classes = await _context.Classes.FirstOrDefaultAsync(c => c.Id == Id);
        if (classes is null) return NotFound();
        return Ok(classes?.ToDto());
    }

    [HttpPost]
    public async Task<IActionResult> CreateClass([FromBody]CreateClassDto createClassDto)
    {
        if (!ModelState.IsValid) return BadRequest();
        var user = await _userManager.GetUserAsync(User);
        
        var classes = new Class()
        {
            Name = createClassDto.Name,
            Key = Guid.NewGuid().ToString("N"),
            UserClasses = new List<UserClasses>()
            {
                new UserClasses()
                {
                    UserId = user.Id,
                    IsAdmin = true
                }
            }
        };

        await _context.Classes.AddAsync(classes);
        await _context.SaveChangesAsync();
        classes = await _context.Classes.FirstOrDefaultAsync(c => c.Id == classes.Id);
        var classDto = classes.ToDto();

        return Ok(classDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClass(Guid Id,[FromBody] UpdateClass updateClass)
    {
        if (!await _context.Classes.AnyAsync(c => c.Id == Id)) return NotFound();
        if (!ModelState.IsValid) return BadRequest();
        var classes = await _context.Classes.FirstOrDefaultAsync(c => c.Id == Id);
        if (classes is null) return NotFound();

        var user = await _userManager.GetUserAsync(User);
        if (classes.UserClasses?.Any(u => u.UserId == user.Id && u.IsAdmin) != true)
            return NotFound();
        classes.Name = updateClass.Name;
        await _context.SaveChangesAsync();
        return Ok(classes);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClass(Guid Id)
    { 
        var classes = await _context.Classes.FirstOrDefaultAsync(c => c.Id == Id);
        if (classes is null) return NotFound();

        var user = await _userManager.GetUserAsync(User);
        if (classes.UserClasses?.Any(u => u.UserId == user.Id && u.IsAdmin) != true)
            return NotFound();

        _context.Classes.Remove(classes);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
