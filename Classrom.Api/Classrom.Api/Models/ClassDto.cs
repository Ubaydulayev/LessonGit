using System;
namespace Classrom.Api.Models
{
	public class ClassDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Key { get; set; }
        public List<UserDto>? Users { get; set; }
    }
}

