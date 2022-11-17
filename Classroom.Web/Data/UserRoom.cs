namespace Classroom.Web.Data;

public class UserRoom
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public Guid RoomId { get; set; }
    public Classroom? Room { get; set; }
}