
using Classroom.Web.Data;
using Classroom.Web.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Classroom.Web.Controllers;

public class ClassroomsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;

    public ClassroomsController(ApplicationDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ClassroomById(Guid id)
    {
        var classroom = await _context.Classrooms.FindAsync(id);

        if (classroom is null)
        {
            return NotFound();
        }

        return View(classroom);
    }

    [HttpGet]
    public IActionResult AddClassroom()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddClassroom(CreateClassroomDto classroomDto)
    {
        if (!ModelState.IsValid)
        {
            return View(classroomDto);
        }

        //save to db

        var classroom = new Classroom.Web.Data.Classroom
        {
            Name = classroomDto.Name
        };

        await _context.Classrooms.AddAsync(classroom);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }


    [HttpGet]
    public IActionResult JoinClassroom()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> JoinClassroom(JoinClassroomDto joinClassroomDto)
    {
        if (!ModelState.IsValid)
        {
            return View(joinClassroomDto);
        }

        var classroom = await _context.Classrooms.FirstOrDefaultAsync(c => c.Key == joinClassroomDto.Key);
        if (classroom is null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);

        if (!classroom.Users.Any(u => u.UserId == user.Id))
        {
            classroom.Users?.Add(new UserRoom()
            {
                RoomId = classroom.Id,
                UserId = user.Id
            });

            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(ClassroomById), new { Id = classroom.Id });
    }
}