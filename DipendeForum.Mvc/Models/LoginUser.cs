using System.ComponentModel.DataAnnotations;
using DipendeForum.Domain;

namespace DipendeForum.Mvc.Models
{
    public class LoginUser
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string ProfilePicture { get; set; }

        public Role Role { get; set; }
    }
}