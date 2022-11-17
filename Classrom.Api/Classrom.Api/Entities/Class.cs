using System;
namespace Classrom.Api.Entities;
public class Class
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Key { get; set; }
    public Guid CourseId { get; set; }
    public virtual List<UserClasses>? UserClasses { get; set; }
} 