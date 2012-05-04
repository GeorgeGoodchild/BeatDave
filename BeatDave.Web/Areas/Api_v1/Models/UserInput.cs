using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace BeatDave.Web.Areas.Api_v1.Models
{
    public class UserInput
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9_]*$")]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [Email]
        public string Email { get; set; }
    }
}

