namespace Classroom.Web.Data;

public class Task
{
    public Guid Id {get; set;}
    public ETaskStatus Status {get; set;}
    public string? Description {get; set;}
    public string? Title {get; set;}
    public string? Url {get; set;}
    public DateTime CreatedDate {get; set;}
    public DateTime? StartDate {get; set;}
    public DateTime? EndDate {get; set;}

    public virtual ICollection<UserTask>? UserTasks {get; set;}
}
