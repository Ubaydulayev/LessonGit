 using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Classrom.Api.Entities
{
	public class UserClasses
    {
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		[ForeignKey(nameof(UserId))]
		public virtual User? User { get; set; }
		public Guid ClassId { get; set; }
		[ForeignKey(nameof(ClassId))]
		public virtual Class? Class { get; set; }
		public bool IsAdmin { get; set; }
	}
}

