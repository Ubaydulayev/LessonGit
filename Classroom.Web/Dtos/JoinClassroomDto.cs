using System.ComponentModel.DataAnnotations;

namespace Classroom.Web.Dtos;

public class JoinClassroomDto
{
    [Required]
    public Guid? Key { get; set; }
}