using Microsoft.AspNetCore.Identity;

namespace Classroom.Web.Data;

public class User : IdentityUser<Guid>
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public ulong ChatId { get; set; }
    public ushort Step { get; set; }
    public string? PhotoUrl { get; set; }
    public EUserStatus Status { get; set; }

    public virtual ICollection<UserRoom>? Rooms { get; set; }
}