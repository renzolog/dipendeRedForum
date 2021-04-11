using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DipendeForum.Context.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Index(IsUnique = true), Required] 
        public string Username { get; set; }

        [Index(IsUnique = true), Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string ProfilePicture { get; set; }

        public string Role { get; set; }

        public virtual List<Post> Posts { get; set; } 

        public virtual List<Message> Messages { get; set; } 
    }
}
