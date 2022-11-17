using System;
using System.ComponentModel.DataAnnotations;

namespace Classrom.Api.Models
{
	public class CreateClassDto
	{
		[Required]
		public string? Name { get; set; }
	}
}

