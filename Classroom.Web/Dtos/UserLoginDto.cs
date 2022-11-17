using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Classroom.Web.Dtos
{
    public class UserLoginDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Remote(action: "IsEmailExists", controller: "Account")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
