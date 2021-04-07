using System.ComponentModel.DataAnnotations;
using DipendeForum.Domain;

namespace DipendeForum.Mvc.Models
{
    public class LoginUser
    {
        public LoginUser(string username, string email, string password, string profilePicture, Role role)
        {
            Username = username;
            Email = email;
            Password = password;
            ProfilePicture = profilePicture;
            Role = role;
        }

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