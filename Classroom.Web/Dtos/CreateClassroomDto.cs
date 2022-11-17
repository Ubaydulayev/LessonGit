using System.ComponentModel.DataAnnotations;

namespace Classroom.Web.Dtos;

public class CreateClassroomDto
{
    [Required]
    [MaxLength(30)]
    public string? Name { get; set; }
}
