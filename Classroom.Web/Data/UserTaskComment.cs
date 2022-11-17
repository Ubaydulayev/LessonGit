namespace Classroom.Web.Data;

public class UserTaskComment
{
    public Guid Id { get; set; }
    public Guid UserTaskId { get; set; }
    public UserTask? UserTask { get; set; }
    public string? Message { get; set; }
    public Guid? UserId { get; set; }
    public User? User { get; set; }
    public DateTime? CreatedAt { get; set; }
}