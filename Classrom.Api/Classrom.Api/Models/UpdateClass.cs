using System;
using System.ComponentModel.DataAnnotations;

namespace Classrom.Api.Models
{
	public class UpdateClass
	{
		[Required]
		public string? Name { get; set; }
	}
}

