using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Classroom.Web.Data;

public class ApplicationDbContext : IdentityDbContext<User, UserRole, Guid>
{
    public DbSet<Classroom> Classrooms { get; set; }
    public DbSet<Task>? Tasks { get; set; }
    public DbSet<UserRoom>? UserRooms { get; set; }
    public DbSet<UserTask>? UserTasks { get; set; }
    public DbSet<UserTaskComment>? UserTasksComment { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
}